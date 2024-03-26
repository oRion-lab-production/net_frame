using EFLayer.Domain;
using EFLayer.Domain.DataModels.Common;
using EFLayer.Domain.IRepositories.Common;
using Microsoft.EntityFrameworkCore;
using Structures.Conn_EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataAccess;

namespace EFLayer.Core.Repositories.Common
{
    public class EFProductRepository : EFRepoBase<Product, AppDbContext>, IEFProductRepository
    {
        public EFProductRepository(EFFactoryBuilder<AppDbContext> dbFactory) : base(dbFactory)
        {
        }
    }
}
