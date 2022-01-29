using System;
using GOfit.MyGOfit.ExceptionMiddleware.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GOfit.MyGOfit.ExceptionMiddleware
{
    /// <summary>
    /// Main exception
    /// </summary>
    public class MyGOfitException : Exception
    {
        public ExceptionType Type;
        public ExceptionRepository Exception;
        public ExceptionEntity Entity;
        public string Detail;
        public ModelStateDictionary ModelState;

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception) : base(CreateMessage(exception)) => PopulateProperties(exceptionType, exception);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            ExceptionEntity exceptionEntity) : base(CreateMessage(exception, exceptionEntity)) => PopulateProperties(exceptionType, exception, exceptionEntity);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            Exception innerException) : base(CreateMessage(exception), innerException) => PopulateProperties(exceptionType, exception);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            ExceptionEntity exceptionEntity,
            Exception innerException) : base(CreateMessage(exception, exceptionEntity), innerException) => PopulateProperties(exceptionType, exception, exceptionEntity);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            string detail) : base(CreateMessage(exception)) => PopulateProperties(exceptionType, exception, detail);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            ExceptionEntity exceptionEntity,
            string detail) : base(CreateMessage(exception, exceptionEntity)) => PopulateProperties(exceptionType, exception, exceptionEntity, detail);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            ExceptionEntity exceptionEntity,
            string detail,
            Exception innerException) : base(CreateMessage(exception, exceptionEntity), innerException) => PopulateProperties(exceptionType, exception, exceptionEntity, detail);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            ExceptionEntity exceptionEntity,
            string detail,
            ModelStateDictionary modelState) : base(CreateMessage(exception, exceptionEntity)) => PopulateProperties(exceptionType, exception, exceptionEntity, detail, modelState);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            string detail,
            ModelStateDictionary modelState,
            Exception innerException) : base(CreateMessage(exception), innerException) => PopulateProperties(exceptionType, exception, detail, modelState);

        public MyGOfitException(ExceptionType exceptionType,
            ExceptionRepository exception,
            ExceptionEntity exceptionEntity,
            string detail,
            ModelStateDictionary modelState,
            Exception innerException) : base(CreateMessage(exception, exceptionEntity), innerException) => PopulateProperties(exceptionType, exception, exceptionEntity, detail, modelState);


        #region POPULATE

        private void PopulateProperties(ExceptionType exceptionType, ExceptionRepository exception, string detail, ModelStateDictionary modelState)
        {
            Type = exceptionType;
            Exception = exception;
            Detail = string.IsNullOrWhiteSpace(detail) ? null : detail;
            ModelState = modelState;
        }

        private void PopulateProperties(ExceptionType exceptionType, ExceptionRepository exception, ExceptionEntity entity, string detail, ModelStateDictionary modelState)
        {
            Type = exceptionType;
            Exception = exception;
            Entity = entity;
            Detail = string.IsNullOrWhiteSpace(detail) ? null : detail;
            ModelState = modelState;
        }
        private void PopulateProperties(ExceptionType exceptionType, ExceptionRepository exception, string detail)
        {
            Type = exceptionType;
            Exception = exception;
            Detail = string.IsNullOrWhiteSpace(detail) ? null : detail;
        }
        private void PopulateProperties(ExceptionType exceptionType, ExceptionRepository exception, ExceptionEntity entity, string detail)
        {
            Type = exceptionType;
            Exception = exception;
            Entity = entity;
            Detail = string.IsNullOrWhiteSpace(detail) ? null : detail;
        }
        private void PopulateProperties(ExceptionType exceptionType, ExceptionRepository exception)
        {
            Type = exceptionType;
            Exception = exception;
        }
        private void PopulateProperties(ExceptionType exceptionType, ExceptionRepository exception, ExceptionEntity entity)
        {
            Type = exceptionType;
            Entity = entity;
            Exception = exception;
        }


        #endregion


        #region CREATE MESSAGE

        private static string CreateMessage(ExceptionRepository exception, ExceptionEntity exceptionEntity)
        {
            if (exceptionEntity == ExceptionEntity.Unknown) return CreateMessage(exception);
            return string.Concat(Common.GetEnumString(exception), ".", Common.GetEnumString(exceptionEntity));
        }

        private static string CreateMessage(ExceptionRepository exception)
        {
            return string.Concat(Common.GetEnumString(exception));
        }

        #endregion
    }
}
