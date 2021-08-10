using System;

namespace BackendCore.Common.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException() : base("Entity not found")
        {

        }
        public EntityNotFoundException(string entityId) : base($"Entity with Id: {entityId} is not found ")
        {

        }
        public EntityNotFoundException(string entityId, Exception innerException) : base($"Entity with Id: {entityId} is not found ", innerException)
        {

        }

    }
}
