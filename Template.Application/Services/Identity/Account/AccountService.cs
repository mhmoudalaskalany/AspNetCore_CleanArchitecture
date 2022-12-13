using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core;
using Common.DTO.Identity.Account;
using Common.DTO.Identity.User;
using Common.Extensions;
using Common.Infrastructure.Repository.ActiveDirectory;
using Microsoft.EntityFrameworkCore;
using Template.Application.Services.Base;

namespace Template.Application.Services.Identity.Account
{
    public class AccountService : BaseService<Domain.Entities.Identity.User,AddUserDto, UserDto, Guid , Guid?>, IAccountService
    {
        private readonly ITokenService _tokenBusiness;
        private readonly IActiveDirectoryRepository _activeDirectoryRepository;
        public AccountService(IServiceBaseParameter<Domain.Entities.Identity.User> businessBaseParameter, ITokenService tokenBusiness, IActiveDirectoryRepository activeDirectoryRepository) : base(businessBaseParameter)
        {
            _tokenBusiness = tokenBusiness;
            _activeDirectoryRepository = activeDirectoryRepository;
        }

        public async Task<IFinalResult> Login(LoginParameters parameters)
        {
            var user = await UnitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName == parameters.Username && !q.IsDeleted, include: source => source.Include(a => a.Role), disableTracking: false);
            if (user == null) return ResponseResult.PostResult(status: HttpStatusCode.BadRequest,
                message: "Wrong Username or Password");
            var rightPass = CryptoHasher.VerifyHashedPassword(user.Password, parameters.Password);
            if (!rightPass) return ResponseResult.PostResult(status: HttpStatusCode.NotFound, message: "Wrong Password");
            var role = user.Role;
            var userDto = Mapper.Map<Domain.Entities.Identity.User, UserDto>(user);
            var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, role);
            return ResponseResult.PostResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
       
        public async Task<IFinalResult> AdLogin(LoginParameters parameters)
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
                var role = user.Role;
                var userDto = Mapper.Map<Domain.Entities.Identity.User, UserDto>(user);

                var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, role);
                return ResponseResult.PostResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        private async Task<Domain.Entities.Identity.User> CheckIfUserInDatabase(ActiveDirectoryUserDto dto)
        {
            try
            {
                var userInDb = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UserName == dto.LogonName && !x.IsDeleted, include: src => src.Include(r => r.Role));
                if (userInDb != null)
                {
                    return userInDb;
                }


                var user = Mapper.Map<ActiveDirectoryUserDto, Domain.Entities.Identity.User>(dto);
                // add default user role to user we can change it after that
                user.RoleId = 2;
                UnitOfWork.Repository.Add(user);
                await UnitOfWork.SaveChangesAsync();
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