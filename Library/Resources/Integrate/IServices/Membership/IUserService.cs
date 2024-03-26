using Layer.Domain.GenericModels.Transformer;
using Layer.Domain.GenericModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrate.IServices.Membership
{
    public interface IUserService
    {
        Task<TransformResponse.ObjectResponseVal> AddAsync(UserCreateDtoModel model);
        Task<TransformResponse.ObjectResponseVal> ReadByEmailAsync(UserLoginDtoModel model);
    }
}
