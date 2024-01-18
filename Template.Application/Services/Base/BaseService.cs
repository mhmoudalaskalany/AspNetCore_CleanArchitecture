using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.Infrastructure.UnitOfWork;
using Template.Domain;
using Template.Domain.Enum;
using Template.Integration.CacheRepository;
using static System.Enum;
namespace Template.Application.Services.Base
{
    public class BaseService<T, TAddDto , TEditDto, TGetDto, TKey, TKeyDto>
        : IBaseService<T, TAddDto , TEditDto, TGetDto, TKey, TKeyDto>
        where T : class
        where TAddDto : IEntityDto<TKeyDto>
        where TEditDto : IEntityDto<TKeyDto>
        where TGetDto : IEntityDto<TKeyDto>
    {
        protected readonly IUnitOfWork<T> UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected IFinalResult Result;
        protected IHttpContextAccessor HttpContextAccessor;
        protected IConfiguration Configuration;
        protected ICacheRepository CacheRepository;
        protected TokenClaimDto ClaimData { get; set; }

        protected internal BaseService(IServiceBaseParameter<T> businessBaseParameter)
        {
            HttpContextAccessor = businessBaseParameter.HttpContextAccessor;
            UnitOfWork = businessBaseParameter.UnitOfWork;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
            CacheRepository = businessBaseParameter.CacheRepository;
            Configuration = businessBaseParameter.Configuration;
            var claims = HttpContextAccessor?.HttpContext?.User;
            ClaimData = GetTokenClaimDto(claims);

        }

        public virtual async Task<IFinalResult> GetByIdAsync(object id)
        {
            T query = await UnitOfWork.Repository.GetAsync(id);
            var data = Mapper.Map<T, TGetDto>(query);
            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK,
                message: MessagesConstants.Success);
        }

        public virtual async Task<IFinalResult> GetEditByIdAsync(object id)
        {
            T query = await UnitOfWork.Repository.GetAsync(id);
            var data = Mapper.Map<T, TEditDto>(query);
            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK,
                message: MessagesConstants.Success);
        }

        public virtual async Task<IFinalResult> GetAllAsync(bool disableTracking = false, Expression<Func<T, bool>> predicate = null)
        {
            IEnumerable<T> query;
            if (predicate != null)
            {
                query = await UnitOfWork.Repository.FindAsync(predicate);
            }
            else
            {
                query = await UnitOfWork.Repository.GetAllAsync(disableTracking: disableTracking);
            }
            var data = Mapper.Map<IEnumerable<T>, IEnumerable<TGetDto>>(query);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }

        public virtual async Task<IFinalResult> AddAsync(TAddDto model)
        {
            T entity = Mapper.Map<TAddDto, T>(model);
            SetEntityCreatedBaseProperties(entity);
            await UnitOfWork.Repository.AddAsync(entity);
            var affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                    message: MessagesConstants.AddSuccess);
            }
            Result.Data = model;
            return Result;
        }

        public virtual async Task<IFinalResult> AddListAsync(List<TAddDto> model)
        {
            var entities = Mapper.Map<List<TAddDto>, List<T>>(model);
            UnitOfWork.Repository.AddRange(entities);
            var affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                    message: MessagesConstants.AddSuccess);
            }
            Result.Data = model;
            return Result;
        }

        public virtual async Task<IFinalResult> UpdateAsync(TAddDto model)
        {
            T entityToUpdate = await UnitOfWork.Repository.GetAsync(model.Id);
            var newEntity = Mapper.Map(model, entityToUpdate);
            SetEntityModifiedBaseProperties(newEntity);
            UnitOfWork.Repository.Update(entityToUpdate, newEntity);
            var affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: MessagesConstants.UpdateSuccess);
            }
            return Result;

        }

        public virtual async Task<IFinalResult> DeleteAsync(object id)
        {
            var entityToDelete = await UnitOfWork.Repository.GetAsync(id);
            UnitOfWork.Repository.Remove(entityToDelete);
            var affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: MessagesConstants.DeleteSuccess);
            }
            return Result;
        }

        public virtual async Task<IFinalResult> DeleteSoftAsync(object id)
        {
            var entityToDelete = await UnitOfWork.Repository.GetAsync(id);
            SetEntityModifiedBaseProperties(entityToDelete);
            UnitOfWork.Repository.RemoveLogical(entityToDelete);
            var affectedRows = await UnitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: MessagesConstants.DeleteSuccess);
            }
            return Result;
        }

        protected void SetEntityCreatedBaseProperties(T entity)
        {
            var type = entity.GetType();
            var createdBy = type.GetProperty("CreatedById");
            if (createdBy != null) createdBy.SetValue(entity, ClaimData.UserId);
            var createdDate = type.GetProperty("CreatedDate");
            if (createdDate != null) createdDate.SetValue(entity, DateTime.UtcNow);

        }

        protected void SetEntityModifiedBaseProperties(T entity)
        {
            var type = entity.GetType();
            var createdBy = type.GetProperty("ModifiedById");
            if (createdBy != null) createdBy.SetValue(entity, ClaimData.UserId);
            var createdDate = type.GetProperty("ModifiedDate");
            if (createdDate != null) createdDate.SetValue(entity, DateTime.UtcNow);

        }


        private TokenClaimDto GetTokenClaimDto(ClaimsPrincipal claims)
        {
            _ = TryParse(claims?.FindFirst(t => t.Type == "UserType")?.Value ?? "1", out UserType userType);
            var claimData = new TokenClaimDto
            {
                UserId = claims?.FindFirst(t => t.Type == "UserId")?.Value,
                Email = claims?.FindFirst(t => t.Type == "Email")?.Value,
                UserType = userType,
                UserName = claims?.FindFirst(t => t.Type == "Username")?.Value
            };
            if (!string.IsNullOrWhiteSpace(claims?.FindFirst(t => t.Type == "UserTypeId")?.Value))
            {
                claimData.UserTypeId = long.Parse(claims.FindFirst(t => t.Type == "UserTypeId")?.Value ?? "0");
            }
            return claimData;
        }

    }
}
