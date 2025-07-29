using System;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Identity.Account;
using Template.Common.DTO.Identity.User;
using Template.Common.Extensions;
using Template.Common.Infrastructure.Repository.ActiveDirectory;
using Microsoft.EntityFrameworkCore;
using Template.Application.Services.Base;
using Template.Domain;

namespace Template.Application.Services.Identity.Account
{
    public class AccountService : BaseService<Domain.Entities.Identity.User,AddUserDto , EditUserDto, UserDto, Guid , Guid?>, IAccountService
    {
        private readonly ITokenService _tokenBusiness;
        private readonly IActiveDirectoryRepository _activeDirectoryRepository;
        public AccountService(IServiceBaseParameter<Domain.Entities.Identity.User> businessBaseParameter, ITokenService tokenBusiness, IActiveDirectoryRepository activeDirectoryRepository) : base(businessBaseParameter)
        {
            _tokenBusiness = tokenBusiness;
            _activeDirectoryRepository = activeDirectoryRepository;
        }

        public async Task<Result<LoginResponse>> Login(LoginParameters parameters)
        {
            var user = await UnitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName == parameters.Username && !q.IsDeleted, include: source => source.Include(a => a.Role), disableTracking: false);

            if (user == null)
            {
                return Result<LoginResponse>.Failure(message: MessagesConstants.WrongUserOrPassword);
            }
            

            var rightPass = CryptoHasher.VerifyHashedPassword(user.Password, parameters.Password);
            if (!rightPass)
            {
                return Result<LoginResponse>.Failure(message: MessagesConstants.WrongUserOrPassword);
            }
            var role = user.Role;

            var userDto = Mapper.Map<Domain.Entities.Identity.User, UserDto>(user);

            var loginResponse = _tokenBusiness.GenerateJsonWebToken(userDto, role);

            return Result<LoginResponse>.Success(loginResponse , MessagesConstants.Success);
        }
       
        public async Task<Result<LoginResponse>> AdLogin(LoginParameters parameters)
        {
            try
            {
                var activeDirectoryUser = _activeDirectoryRepository.LoginAsync(parameters);
                if (activeDirectoryUser == null)
                {
                    return Result<LoginResponse>.Failure(message: MessagesConstants.WrongUserOrPassword);

                }
                var user = await CheckIfUserInDatabase(activeDirectoryUser);
                var role = user.Role;
                var userDto = Mapper.Map<Domain.Entities.Identity.User, UserDto>(user);

                var loginResponse = _tokenBusiness.GenerateJsonWebToken(userDto, role);

                return Result<LoginResponse>.Success(loginResponse, MessagesConstants.Success);
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
                await UnitOfWork.Repository.AddAsync(user);
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