﻿using System.Diagnostics.CodeAnalysis;

namespace Template.Domain
{
    [ExcludeFromCodeCoverage]
    public static  class MessagesConstants
    {
        public const string Success = "SUCCESS";

        public const string GetError = "GET_ERROR";

        public const string EntityNotFound = "ENTITY_NOT_FOUND";

        public const string AddSuccess = "ADD_SUCCESS";

        public const string AddError = "ADD_ERROR";

        public const string UpdateSuccess = "UPDATE_SUCCESS";

        public const string UpdateError = "UPDATE_ERROR";

        public const string DeleteSuccess = "DELETE_ERROR";

        public const string DeleteError = "DELETE_SUCCESS";

        public const string NotFound = "NOT_FOUND";

        public const string WrongUserOrPassword = "WRONG_USER_OR_PASSWORD";

        public const string EntitiesRetrievalError = "ENTITIES_RETRIEVAL_ERROR";

        public const string ApplyingNewMigrations = "APPLYING_NEW_MIGRATIONS_TO_DATABASE";

        public const string NoNewMigrations = "NO_NEW_MIGRATIONS_TO_APPLY";

    }
}
