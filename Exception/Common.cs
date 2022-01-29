using System;
using GOfit.MyGOfit.ExceptionMiddleware.Enums;

namespace GOfit.MyGOfit.ExceptionMiddleware
{
    public static class Common
    {
        /// <summary>
        /// Get the name of enum (ExceptionRepository)
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetEnumString(ExceptionRepository @enum) => Enum.GetName(typeof(ExceptionRepository), @enum);

        /// <summary>
        /// Get the name of enum (ExceptionType)
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetEnumString(ExceptionType @enum) => Enum.GetName(typeof(ExceptionType), @enum);
        /// <summary>
        /// Get the name of enum (ExceptionEntity)
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetEnumString(ExceptionEntity @enum) => Enum.GetName(typeof(ExceptionEntity), @enum);
    }
}
