using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
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
        protected IHttpContextAccessor HttpContextAccessor;
        protected IConfiguration Configuration;
        protected ICacheRepository CacheRepository;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        protected TokenClaimDto ClaimData { get; set; }

        protected internal BaseService(IServiceBaseParameter<T> businessBaseParameter)
        {
            HttpContextAccessor = businessBaseParameter.HttpContextAccessor;
            UnitOfWork = businessBaseParameter.UnitOfWork;
            Mapper = businessBaseParameter.Mapper;
            _logger = businessBaseParameter.Logger;
            CacheRepository = businessBaseParameter.CacheRepository;
            Configuration = businessBaseParameter.Configuration;
            var claims = HttpContextAccessor?.HttpContext?.User;
            ClaimData = GetTokenClaimDto(claims);

        }

        public virtual async Task<Result<TGetDto>> GetByIdAsync(object id)
        {
            try
            {
                T query = await UnitOfWork.Repository.GetAsync(id);
                if (query == null)
                {
                    return Result<TGetDto>.Failure(MessagesConstants.EntityNotFound);
                }
                var data = Mapper.Map<T, TGetDto>(query);
                return Result<TGetDto>.Success(data, MessagesConstants.Success);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByIdAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result<TGetDto>.Failure(MessagesConstants.GetError);
            }
        }


        public virtual async Task<Result<TEditDto>> GetEditByIdAsync(object id)
        {
            try
            {
                T query = await UnitOfWork.Repository.GetAsync(id);
                if (query == null)
                {
                    return Result<TEditDto>.Failure(MessagesConstants.EntityNotFound);
                }
                var data = Mapper.Map<T, TEditDto>(query);
                return Result<TEditDto>.Success(data, MessagesConstants.Success);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetEditByIdAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result<TEditDto>.Failure(MessagesConstants.GetError);
            }
        }

        public virtual async Task<Result<IEnumerable<TGetDto>>> GetAllAsync(bool disableTracking = false, Expression<Func<T, bool>> predicate = null)
        {
            try
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
                return Result<IEnumerable<TGetDto>>.Success(data, MessagesConstants.Success);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result<IEnumerable<TGetDto>>.Failure(MessagesConstants.EntitiesRetrievalError);
            }
        }

        public virtual async Task<Result<TKeyDto>> AddAsync(TAddDto model)
        {
            try
            {
                T entity = Mapper.Map<TAddDto, T>(model);
                SetEntityCreatedBaseProperties(entity);
                await UnitOfWork.Repository.AddAsync(entity);
                var affectedRows = await UnitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    return Result<TKeyDto>.Success(model.Id, MessagesConstants.AddSuccess);
                }
                return Result<TKeyDto>.Failure(MessagesConstants.AddError);
            }
            catch (Exception e)
            {
                _logger.LogError($"{MessagesConstants.AddError}-{nameof(AddAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result<TKeyDto>.Failure(MessagesConstants.AddError);
            }
        }

        public virtual async Task<Result<IEnumerable<TKeyDto>>> AddListAsync(List<TAddDto> model)
        {
            try
            {
                List<T> entities = Mapper.Map<List<TAddDto>, List<T>>(model);
                await UnitOfWork.Repository.AddRangeAsync(entities);
                var affectedRows = await UnitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    var ids = model.Select(x => x.Id);
                    return Result<IEnumerable<TKeyDto>>.Success(ids, MessagesConstants.AddSuccess);
                }
                return Result<IEnumerable<TKeyDto>>.Failure(MessagesConstants.AddError);
            }
            catch (Exception e)
            {
                _logger.LogError($"{MessagesConstants.AddError}-{nameof(AddListAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result<IEnumerable<TKeyDto>>.Failure(MessagesConstants.AddError);
            }
        }

        public virtual async Task<Result<TKeyDto>> UpdateAsync(TAddDto model)
        {
            try
            {
                T entityToUpdate = await UnitOfWork.Repository.GetAsync(model.Id);
                if (entityToUpdate == null)
                {
                    return Result<TKeyDto>.Failure(MessagesConstants.EntityNotFound);
                }
                var newEntity = Mapper.Map(model, entityToUpdate);
                SetEntityModifiedBaseProperties(newEntity);
                UnitOfWork.Repository.Update(entityToUpdate, newEntity);
                var affectedRows = await UnitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    return Result<TKeyDto>.Success(model.Id, MessagesConstants.UpdateSuccess);
                }
                return Result<TKeyDto>.Failure(MessagesConstants.UpdateError);
            }
            catch (Exception e)
            {
                _logger.LogError($"{MessagesConstants.UpdateError}-{nameof(UpdateAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result<TKeyDto>.Failure(MessagesConstants.UpdateError);
            }
        }

        public virtual async Task<Result> DeleteAsync(object id)
        {
            try
            {
                var entityToDelete = await UnitOfWork.Repository.GetAsync(id);
                if (entityToDelete == null)
                {
                    return Result.Failure(MessagesConstants.EntityNotFound);
                }
                UnitOfWork.Repository.Remove(entityToDelete);
                var affectedRows = await UnitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    return Result.Success(MessagesConstants.DeleteSuccess);
                }
                return Result.Failure(MessagesConstants.DeleteError);
            }
            catch (Exception e)
            {
                _logger.LogError($"{MessagesConstants.DeleteError}-{nameof(DeleteAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result.Failure(MessagesConstants.DeleteError);
            }
        }

        public virtual async Task<Result> DeleteSoftAsync(object id)
        {
            try
            {
                var entityToDelete = await UnitOfWork.Repository.GetAsync(id);
                if (entityToDelete == null)
                {
                    return Result.Failure(MessagesConstants.EntityNotFound);
                }
                SetEntityModifiedBaseProperties(entityToDelete);
                UnitOfWork.Repository.RemoveLogical(entityToDelete);
                var affectedRows = await UnitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    return Result.Success(MessagesConstants.DeleteSuccess);
                }
                return Result.Failure(MessagesConstants.DeleteError);
            }
            catch (Exception e)
            {
                _logger.LogError($"{MessagesConstants.DeleteError}-{nameof(DeleteSoftAsync)}");
                _logger.LogError(JsonConvert.SerializeObject(e, _serializerSettings));
                return Result.Failure(MessagesConstants.DeleteError);
            }
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
