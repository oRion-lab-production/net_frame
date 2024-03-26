using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.GenericModels.Transformer
{
    public class UserTransformModel
    {
        public UserTransformModel() { }
    }

    public class UserCreateDtoModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserCreateDtoModel() { }
    }

    public class UserReadDtoModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public UserReadDtoModel() { }
    }

    public class UserLoginDtoModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLoginDtoModel() { }
    }

    public class UserTokenDtoModel
    {
        public string Token { get; set; }

        public UserTokenDtoModel() { }
    }
}
