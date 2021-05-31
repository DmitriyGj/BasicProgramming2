using System;
using System.Linq;
using System.Reflection;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        public string GetApiDescription()
        => typeof(T).GetCustomAttributes()?.OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
            

        public string[] GetApiMethodNames()=>
                typeof(T).GetMethods()
                .Where(method=>method.GetCustomAttributes().OfType<ApiMethodAttribute>().Any())
                .Select(method=> method.Name).ToArray();

        public string GetApiMethodDescription(string methodName)=>
                typeof(T).GetMethod(methodName)?
                .GetCustomAttributes().OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description ;

        public string[] GetApiMethodParamNames(string methodName)=>
                typeof(T).GetMethod(methodName).GetParameters().Select(param => param.Name).ToArray();

        public string GetApiMethodParamDescription(string methodName, string paramName)
        {
            return typeof(T).GetMethod(methodName)?.GetParameters()
                .Where(param=>param.Name.Equals(paramName))
                .Select(param=>param).FirstOrDefault()?
                .GetCustomAttributes().OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var res = new ApiParamDescription();
            var method = typeof(T).GetMethod(methodName);
            var atributes = method?.GetParameters()?
                            .Where(param => param.Name == paramName).FirstOrDefault()?.GetCustomAttributes();

            if (method is null || atributes is null)
            {
                res.ParamDescription = new CommonDescription(paramName);
                return res;
            }

            res.ParamDescription = new CommonDescription(paramName, GetApiMethodParamDescription(methodName, paramName)); 
            res.MinValue = atributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MinValue;
            res.MaxValue = atributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MaxValue;
            res.Required = atributes?.OfType<ApiRequiredAttribute>().FirstOrDefault()?.Required ?? false;
            return res;
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var method = typeof(T).GetMethod(methodName);
            if (method.GetCustomAttributes().OfType<ApiMethodAttribute>().FirstOrDefault() is null)
                return null;
            var parameters = method?.GetParameters()?
                             .Select(param => GetApiMethodParamFullDescription(methodName, param.Name))?.ToArray();
            var description = new CommonDescription
            {
                Name = methodName,
                Description = GetApiMethodDescription(methodName)
            };
            if (description is null)
                return null;
            var res = new ApiMethodDescription
            {
                MethodDescription = description,
                ParamDescriptions = parameters,
                ReturnDescription = GetReturnDescription(method)
            };
            return res;
        }

        public static ApiParamDescription GetReturnDescription(MethodInfo method)
        {
            var invalidAttribute = method?.ReturnParameter.GetCustomAttributes().OfType<ApiIntValidationAttribute>()?.FirstOrDefault();
            var reqAttribute = method?.ReturnParameter.GetCustomAttributes().OfType<ApiRequiredAttribute>()?.FirstOrDefault();
            if (reqAttribute is null && invalidAttribute is null)
                return null;
            var res = new ApiParamDescription
            {
                MaxValue = invalidAttribute?.MaxValue,
                MinValue = invalidAttribute?.MinValue,
                Required = reqAttribute?.Required ?? false
            };
            return res;
        }
    }
}