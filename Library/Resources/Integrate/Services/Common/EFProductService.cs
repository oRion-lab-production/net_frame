using EFLayer.Domain.DataModels.Common;
using EFLayer.Domain.IRepositories.Common;
using Integrate.IServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataAccess;

namespace Integrate.Services.Common
{
    public class EFProductService : IEFProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IEFProductRepository _productRepo;

        public EFProductService(IUnitOfWork uow, IEFProductRepository productRepo) 
        { 
            _uow = uow;
            _productRepo = productRepo;
        }

        public IEnumerable<Product> Get()
        {
            return _productRepo.List(x => x.IsActive == true).ToList();
        }
    }
}
