using System;
using System.Diagnostics.CodeAnalysis;

namespace Common.Exceptions
{
    [ExcludeFromCodeCoverage]
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
