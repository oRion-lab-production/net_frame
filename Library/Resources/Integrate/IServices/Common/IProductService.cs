using Layer.Domain.DataModels.Common;
using Layer.Domain.GenericModels;
using Layer.Domain.GenericModels.Transformer;
using Layer.Domain.LibrariesModels.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrate.IServices.Common
{
    public interface IProductService
    {
        Product GetProductById(Guid id);
        Task<Product> GetProductByIdAsync(Guid id);

        IList<Product> ListProduct();
        Task<IList<Product>> ListProductAsync();
        Task<IList<Product>> ListProductAsync(CancellationToken cancellationToken);
        IList<Product> ListLockProduct();
        Task<IList<Product>> ListLockProductAsync();

        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);

        Task CreateProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(Product product, CancellationToken cancellationToken);

        void CreateProducts(List<Product> products);
        void UpdatProducts(List<Product> products);
        void DeleteProducts(List<Product> products);

        Task CreateProductsAsync(List<Product> products);
        Task UpdateProductsAsync(List<Product> products);
        Task DeleteProductsAsync(List<Product> products);
        Task CreateProductsAsync(List<Product> products, CancellationToken cancellationToken);
        Task UpdateProductsAsync(List<Product> products, CancellationToken cancellationToken);
        Task DeleteProductsAsync(List<Product> products, CancellationToken cancellationToken);

        Task<TransformResponse.ObjectResponseVal> DTProcessingProductAsync(DataTable_postModel postModel);
        Task<TransformResponse.ObjectResponseVal> ReadAsync(Guid id);
        Task<TransformResponse.ObjectResponseVal> AddAsync(ProductCreateDtoModel model);
        Task<TransformResponse.ObjectResponseVal> EditAsync(Guid id, ProductUpdateDtoModel model);
        Task<TransformResponse.ObjectResponseVal> RemoveAsync(Guid id);

    }
}
