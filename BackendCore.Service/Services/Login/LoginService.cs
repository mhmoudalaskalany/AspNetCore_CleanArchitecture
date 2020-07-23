using System.Net;
using System.Threading.Tasks;
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
        public LoginService(IServiceBaseParameter<Entities.Entities.User , long> businessBaseParameter, ITokenService tokenBusiness) : base(businessBaseParameter)
        {
            _tokenBusiness = tokenBusiness;
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
    }
}