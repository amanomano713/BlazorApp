using System.Collections.Generic;
using GOfit.MyGOfit.ExceptionMiddleware.Enums;
using GOfit.MyGOfit.ExceptionMiddleware.Output;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GOfit.MyGOfit.ExceptionMiddleware
{
    public static class ExceptionBuilder
    {
        private const string ExceptionLiteral = "Exception";
        public const string I18nExceptionLiteral = "General.Exception.I18nName";
        public const string I18nEntitiesLiteral = "General.Entities.I18nName";

        /// <summary>
        /// Build exception object ready to be serialized
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="exception"></param>
        /// <param name="exceptionDetail"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ExceptionResponse BuildException(ExceptionType exceptionType, ExceptionRepository exception, string exceptionDetail, ModelStateDictionary modelState)
        {
            return BuildException(exceptionType, exception, ExceptionEntity.Unknown, exceptionDetail, modelState);
        }
        public static ExceptionResponse BuildException(ExceptionType exceptionType, ExceptionRepository exception, ExceptionEntity entity, string exceptionDetail, ModelStateDictionary modelState) => new ExceptionResponse()
        {
            Type = $"{ExceptionLiteral}.{Common.GetEnumString(exceptionType)}",
            Exception = $"{ExceptionLiteral}.{Common.GetEnumString(exceptionType)}.{Common.GetEnumString(exception)}",
            Validations = BuildValidations(modelState),
            I18nException = $"{I18nExceptionLiteral}.{Common.GetEnumString(exception)}",
            I18nEntity = $"{I18nEntitiesLiteral}.{Common.GetEnumString(entity)}",
            Entity = Common.GetEnumString(entity),
            Detail = exceptionDetail
        };
        /// <summary>
        /// Go through model state object and build a list of errors
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        private static IEnumerable<PropertyResultResponse> BuildValidations(ModelStateDictionary modelStateDictionary)
        {
            if (modelStateDictionary == null) return null;

            var properties = new List<PropertyResultResponse>();

            foreach (var key in modelStateDictionary.Keys)
            {
                var property = new PropertyResultResponse() { Property = key };

                foreach (var error in modelStateDictionary[key].Errors)
                {
                    property.Errors.Add(error.ErrorMessage);
                }

                properties.Add(property);
            }

            return properties;
        }

    }
}
