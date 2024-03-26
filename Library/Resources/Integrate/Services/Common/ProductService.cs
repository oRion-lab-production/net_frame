using Integrate.IServices.Common;
using Layer.Domain.DataModels.Common;
using Layer.Domain.DataModels.Components;
using Layer.Domain.GenericModels;
using Layer.Domain.GenericModels.Transformer;
using Layer.Domain.IRepositories.Common;
using Layer.Domain.LibrariesModels.DataTable;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq.Clauses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataAccess;
using Tools.Util;

namespace Integrate.Services.Common
{
    public class ProductService : IProductService
    {
        private ILogger<ProductService> _logger;

        private IUnitOfWork _uow;

        private IProductRepository _productRepo;

        public ProductService(ILogger<ProductService> logger, IUnitOfWork uow, IProductRepository productRepo)
        {
            _logger = logger;
            _uow = uow;
            _productRepo = productRepo;
        }

        public Product GetProductById(Guid id) => _productRepo.Get(id);
        public async Task<Product> GetProductByIdAsync(Guid id) => await _productRepo.GetAsync(id);

        public IList<Product> ListProduct() => _productRepo.List();
        public async Task<IList<Product>> ListProductAsync() => await _productRepo.ListAsync();
        public async Task<IList<Product>> ListProductAsync(CancellationToken cancellationToken) => await _productRepo.ListAsync(cancellationToken);
        public IList<Product> ListLockProduct() => _productRepo.ListLock();
        public async Task<IList<Product>> ListLockProductAsync() => await _productRepo.ListLockAsync();

        public void CreateProduct(Product product) => _productRepo.Save(product);
        public void UpdateProduct(Product product) => _productRepo.Update(product);
        public void DeleteProduct(Product product) => _productRepo.Delete(product);

        public async Task CreateProductAsync(Product product) => await _productRepo.SaveAsync(product);
        public async Task UpdateProductAsync(Product product) => await _productRepo.UpdateAsync(product);
        public async Task DeleteProductAsync(Product product) => await _productRepo.DeleteAsync(product);

        public async Task CreateProductAsync(Product product, CancellationToken cancellationToken) => await _productRepo.SaveAsync(product, cancellationToken);
        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken) => await _productRepo.UpdateAsync(product, cancellationToken);
        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken) => await _productRepo.DeleteAsync(product, cancellationToken);

        public void CreateProducts(List<Product> products) => products.ForEach(x => CreateProduct(x));
        public void UpdatProducts(List<Product> products) => products.ForEach(x => UpdateProduct(x));
        public void DeleteProducts(List<Product> products) => products.ForEach(x => DeleteProduct(x));

        public async Task CreateProductsAsync(List<Product> products)
        {
            foreach (var product in products)
                await CreateProductAsync(product);
        }

        public async Task UpdateProductsAsync(List<Product> products)
        {
            foreach (var product in products)
                await UpdateProductAsync(product);
        }

        public async Task DeleteProductsAsync(List<Product> products)
        {
            foreach (var product in products)
                await DeleteProductAsync(product);
        }

        public async Task CreateProductsAsync(List<Product> products, CancellationToken cancellationToken)
        {
            foreach (var product in products)
                await CreateProductAsync(product, cancellationToken);
        }

        public async Task UpdateProductsAsync(List<Product> products, CancellationToken cancellationToken)
        {
            foreach (var product in products)
                await UpdateProductAsync(product, cancellationToken);
        }

        public async Task DeleteProductsAsync(List<Product> products, CancellationToken cancellationToken)
        {
            foreach (var product in products)
                await DeleteProductAsync(product, cancellationToken);
        }

        public async Task<TransformResponse.ObjectResponseVal> DTProcessingProductAsync(DataTable_postModel postModel)
        {
            var responseModel = new TransformResponse.ObjectResponseVal();

            try {
                var orCriterions = new List<ICriterion>();
                var andCriterions = new List<ICriterion>();

                var modelCriteria = _productRepo.Criteria()
                    .SetFirstResult(postModel.start).SetMaxResults(postModel.length);
                var countCriteria = _productRepo.CountCriteria();

                static ICriterion requestedCriteria(string colName, string colVal)
                {
                    return colName switch {
                        nameof(Product.IsActive) => Restrictions.Like(Projections.Conditional(
                            Restrictions.Eq(colName, true), Projections.Constant("Active"), Projections.Constant("Inactive")
                            ), $"%{colVal}%"),
                        nameof(Product.Price) => Restrictions.Like(Projections.Cast(NHibernateUtil.String, Projections.Property("Price")), $"%{colVal}%"),
                        nameof(Product.Quantity) => int.TryParse(colVal, out int parsedQuantity) 
                            ? Restrictions.Eq(Projections.Property("Quantity"), parsedQuantity) 
                            : Restrictions.Like(Projections.Cast(NHibernateUtil.String, Projections.Property("Quantity")), $"%{colVal}%"),
                        _ => Restrictions.Like(colName, $"%{colVal}%")
                    };
                }

                if (postModel.columns != null && postModel.columns.Any()) {
                    foreach (var column in postModel.columns) {
                        if (column.searchable) {
                            if (!string.IsNullOrEmpty(postModel?.search?.value))
                                orCriterions.Add(requestedCriteria(column.name, postModel.search.value));

                            if (!string.IsNullOrEmpty(column?.search?.value))
                                andCriterions.Add(requestedCriteria(column.name, column.search.value));
                        }

                        if (column.orderable) {
                            if (postModel.order != null && postModel.order.Any()) {
                                var order = postModel.order.SingleOrDefault(x => x.column == postModel.columns.IndexOf(column));
                                if (order != null)
                                    modelCriteria.AddOrder(order.dir == "asc" ? Order.Asc(column.name) : Order.Desc(column.name));
                            }
                        }
                    }

                    if (postModel.order == null || !postModel.order.Any())
                        modelCriteria.AddOrder(Order.Asc("Name"));
                }

                if (orCriterions.Any()) {
                    modelCriteria.DisjunctionsAdd(orCriterions);
                    countCriteria.DisjunctionsAdd(orCriterions);
                }

                if (andCriterions.Any()) {
                    modelCriteria.ConjunctionsAdd(andCriterions);
                    countCriteria.ConjunctionsAdd(andCriterions);
                }

                var Products = await modelCriteria.Future<Product>().GetEnumerableAsync();

                var requestedVal = new DT_requestedValue<ProductReadDtoModel>() {
                    Count = await countCriteria.FutureValue<int>().GetValueAsync(),
                    Model = Products.Select(x => new ProductReadDtoModel() {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        IsActive = x.IsActive
                    }).ToList()
                };

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Success, "success", requestedVal);
            } catch (Exception ex) {
                _logger.LogError(ex, ex.Message);

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, ex.Message);
            }

            return responseModel;
        }

        public async Task<TransformResponse.ObjectResponseVal> ReadAsync(Guid id)
        {
            var responseModel = new TransformResponse.ObjectResponseVal();

            try {
                var product = await GetProductByIdAsync(id);

                if (product == null) {
                    responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, $"Product not found for id - {id}");
                } else {
                    responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Success, "success", new ProductReadDtoModel() { 
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        IsActive = product.IsActive
                    });
                }
            } catch (Exception ex) {
                _logger.LogError(ex, ex.Message);

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, ex.Message);
            }

            return responseModel;
        }

        public async Task<TransformResponse.ObjectResponseVal> AddAsync(ProductCreateDtoModel model)
        {
            var responseModel = new TransformResponse.ObjectResponseVal();

            _uow.BeginTransaction();

            try {
                await CreateProductAsync(new Product() {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Record = new RecordTransaction() {
                        CreatedBy = "annonymous"
                    }
                });

                await _uow.FlushAsync();
                _uow.Clear();
                await _uow.CommitAsync();

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Success, "success");
            } catch (Exception ex) {
                await _uow.RollBackAsync();
                _logger.LogError(ex, ex.Message);

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, ex.Message);
            }

            return responseModel;
        }

        public async Task<TransformResponse.ObjectResponseVal> EditAsync(Guid id, ProductUpdateDtoModel model)
        {
            var responseModel = new TransformResponse.ObjectResponseVal();

            _uow.BeginTransaction();

            try {
                var product = await GetProductByIdAsync(id);

                if (product == null) {
                    responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, $"Product unfound for id - {id}");
                    return responseModel;
                }

                product.Name = model.Name;
                product.Description = model.Description;    
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.Record.ModifiedBy = "annonymous";
                product.Record.ModifiedDateTime = DateTime.Now;

                await UpdateProductAsync(product);

                await _uow.FlushAsync();
                _uow.Clear();
                await _uow.CommitAsync();

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Success, "success");
            } catch (Exception ex) {
                await _uow.RollBackAsync();
                _logger.LogError(ex, ex.Message);

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, ex.Message);
            }

            return responseModel;
        }

        public async Task<TransformResponse.ObjectResponseVal> RemoveAsync(Guid id)
        {
            var responseModel = new TransformResponse.ObjectResponseVal();

            _uow.BeginTransaction();

            try {
                var product = await GetProductByIdAsync(id);

                if(product == null) {
                    responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, $"Product unfound for id - {id}");
                    return responseModel;
                }

                await DeleteProductAsync(product);

                await _uow.FlushAsync();
                _uow.Clear();
                await _uow.CommitAsync();

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Success, "success");
            } catch (Exception ex) {
                await _uow.RollBackAsync();
                _logger.LogError(ex, ex.Message);

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, ex.Message);
            }

            return responseModel;
        }
    }
}
