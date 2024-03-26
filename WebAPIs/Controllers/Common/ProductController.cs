using Integrate.IServices.Common;
using Layer.Domain.DataModels.Common;
using Layer.Domain.GenericModels.Transformer;
using Layer.Domain.LibrariesModels.DataTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPIs.Controllers.Common
{
    public class ProductController : APIBase
    {
        private IProductService _productSvc;
        private IEFProductService _efProductSvc;

        public ProductController(ILogger<ProductController> logger, IProductService productSvc, IEFProductService efProductSvc) : base(logger)
        {
            _productSvc = productSvc;   
            _efProductSvc = efProductSvc;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<EFLayer.Domain.DataModels.Common.Product> Get()
        {
            return _efProductSvc.Get();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessTable(DataTable_postModel model) => Ok(await _productSvc.DTProcessingProductAsync(model));

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDtoModel model) => Ok(await _productSvc.AddAsync(model));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Read(Guid id) => Ok(await _productSvc.ReadAsync(id));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, ProductUpdateDtoModel model) => Ok(await _productSvc.EditAsync(id, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id) => Ok(await _productSvc.RemoveAsync(id));

    }
}
