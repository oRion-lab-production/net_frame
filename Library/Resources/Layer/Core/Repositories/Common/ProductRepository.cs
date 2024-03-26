using Layer.Domain.DataModels.Common;
using Layer.Domain.IRepositories.Common;
using Structures.Conn_nHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Repositories.Common
{
    public class ProductRepository : RepoBase<Product>, IProductRepository
    {
        public ProductRepository(IAppSessionBuilder sessionBuilder) 
            : base(sessionBuilder) { }
    }
}
