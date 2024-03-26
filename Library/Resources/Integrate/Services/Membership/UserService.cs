using Integrate.IServices.Membership;
using Layer.Domain.DataModels.Components;
using Layer.Domain.GenericModels.Transformer;
using Layer.Domain.GenericModels;
using Layer.Domain.IRepositories.Membership;
using Microsoft.Extensions.Logging;
using NHibernate.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataAccess;
using Layer.Domain.DataModels.Membership;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integrate.Services.Membership
{
    public class UserService : IUserService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        private readonly ILogger<UserService> _logger;

        private readonly IUnitOfWork _uow;

        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;

        public UserService(IServiceProvider serviceProvider, ILogger<UserService> logger, IUnitOfWork uow, IUserRepository userRepo, IRoleRepository roleRepo) 
        {
            _serviceProvider = serviceProvider;
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _logger = logger;

            _uow = uow;

            _userRepo = userRepo;   
            _roleRepo = roleRepo;
        }

        public async Task<TransformResponse.ObjectResponseVal> AddAsync(UserCreateDtoModel model)
        {
            var responseModel = new TransformResponse.ObjectResponseVal();

            _uow.BeginTransaction();

            try {
                var user = await _userRepo.GetUserByNameOrEmail(model.Name, model.Email);

                if(user != null) {
                    responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, "User has registered. Please proceed with login.");
                } else {
                    var role = await _roleRepo.GetRoleByName("User");

                    byte[] encData_byte = new byte[model.Password.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(model.Password);
                    string encodedPassword = Convert.ToBase64String(encData_byte);

                    await _userRepo.SaveAsync(new User() {
                        Name = model.Name,
                        Email = model.Email,
                        Password = encodedPassword,
                        Role = role,
                        Record = new RecordTransaction() {
                            CreatedBy = "annonymous"
                        }
                    });

                    await _uow.FlushAsync();
                    _uow.Clear();
                    await _uow.CommitAsync();

                    responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Success, "success");
                }
            } catch (Exception ex) {
                await _uow.RollBackAsync();
                _logger.LogError(ex, ex.Message);

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, ex.Message);
            }

            return responseModel;
        }

        public async Task<TransformResponse.ObjectResponseVal> ReadByEmailAsync(UserLoginDtoModel model)
        {
            var responseModel = new TransformResponse.ObjectResponseVal();

            try {
                var user = await _userRepo.GetUserByEmail(model.Email);

                if (user == null) {
                    responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, "User not been registered, please proceed with register.");
                } else {
                    byte[] encData_byte = new byte[model.Password.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(model.Password);
                    string encodedPassword = Convert.ToBase64String(encData_byte);

                    if (user.Password != encodedPassword) {
                        responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, "Email or password combination wrong. Please try again.");
                    } else {
                        responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Success, "success", GenerateJSONWebToken(user));
                    }
                }
            } catch (Exception ex) {
                await _uow.RollBackAsync();
                _logger.LogError(ex, ex.Message);

                responseModel.SetObjectResponseVal(ConstantValue.OperationResult.Failure, ex.Message);
            }

            return responseModel;
        }

        private string GenerateJSONWebToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }), 
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

    }
}
