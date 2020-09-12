using System;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Abstraction.Repository.ActiveDirectory;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Login;
using BackendCore.Common.DTO.User;
using BackendCore.Common.Extensions;
using BackendCore.Service.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BackendCore.Service.Services.Login
{
    public class LoginService : BaseService<Entities.Entities.User,AddUserDto, UserDto, long , long?>, ILoginService
    {
        private readonly ITokenService _tokenBusiness;
        private readonly IActiveDirectoryRepository _activeDirectoryRepository;
        public LoginService(IServiceBaseParameter<Entities.Entities.User , long> businessBaseParameter, ITokenService tokenBusiness, IActiveDirectoryRepository activeDirectoryRepository) : base(businessBaseParameter)
        {
            _tokenBusiness = tokenBusiness;
            _activeDirectoryRepository = activeDirectoryRepository;
        }
        public async Task<IResult> Login(LoginParameters parameters)
        {
            var user = await UnitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName == parameters.Username && !q.IsDeleted, include: source => source.Include(a => a.Role), disableTracking: false);
            if (user == null) return ResponseResult.PostResult(status: HttpStatusCode.BadRequest,
                message: "Wrong Username or Password");
            bool rightPass = CryptoHasher.VerifyHashedPassword(user.Password, parameters.Password);
            if (!rightPass) return ResponseResult.PostResult(status: HttpStatusCode.NotFound, message: "Wrong Password");
            var role = user.RoleId;
            var userDto = Mapper.Map<Entities.Entities.User, UserDto>(user);
            var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, role.ToString());
            return ResponseResult.PostResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResult> AdLogin(LoginParameters parameters)
        {
            try
            {
                var activeDirectoryUser = _activeDirectoryRepository.LoginAsync(parameters);
                if (activeDirectoryUser == null)
                {
                    return ResponseResult.PostResult(status: HttpStatusCode.BadRequest,
                        message: "Wrong Username or Password");

                }
                var user = await CheckIfUserInDatabase(activeDirectoryUser);
                var role = user.RoleId;
                var userDto = Mapper.Map<Entities.Entities.User, UserDto>(user);

                var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, role.ToString());
                return ResponseResult.PostResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<Entities.Entities.User> CheckIfUserInDatabase(ActiveDirectoryUserDto dto)
        {
            try
            {
                var userInDb = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UserName == dto.LogonName && !x.IsDeleted, include: src => src.Include(r => r.Role));
                if (userInDb != null)
                {
                    return userInDb;
                }


                var user = Mapper.Map<ActiveDirectoryUserDto, Entities.Entities.User>(dto);
                // add default user role to user we can change it after that
                user.RoleId = 2;
                UnitOfWork.Repository.Add(user);
                await UnitOfWork.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}