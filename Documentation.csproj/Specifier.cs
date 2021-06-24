using System;
using System.Linq;
using System.Reflection;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        public string GetApiDescription()
        {
            return typeof(T).GetCustomAttributes()?.OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        } 

        public string[] GetApiMethodNames()
        {
            return  typeof(T).GetMethods()
                .Where(method=>method.GetCustomAttributes().OfType<ApiMethodAttribute>().Any())
                .Select(method=> method.Name).ToArray();
        }

        public string GetApiMethodDescription(string methodName)
        {
            return typeof(T).GetMethod(methodName)?
                .GetCustomAttributes().OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description ;
        }

        public string[] GetApiMethodParamNames(string methodName)
        {
            return typeof(T).GetMethod(methodName)?.GetParameters().Select(param => param.Name).ToArray();
        }
                
        public string GetApiMethodParamDescription(string methodName, string paramName)
        {
            return typeof(T).GetMethod(methodName)?.GetParameters()
                .Where(param=>param.Name.Equals(paramName))
                .Select(param=>param).FirstOrDefault()?
                .GetCustomAttributes().OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var paramDescription = new ApiParamDescription();
            var method = typeof(T).GetMethod(methodName);
            var attributes = method?.GetParameters()
                            .Where(param => param.Name == paramName).FirstOrDefault()?.GetCustomAttributes();

            if (method is null || attributes is null)
            {
                paramDescription.ParamDescription = new CommonDescription(paramName);
                return paramDescription;
            }
            paramDescription.ParamDescription = new CommonDescription(paramName, GetApiMethodParamDescription(methodName, paramName)); 
            paramDescription.MinValue = attributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MinValue;
            paramDescription.MaxValue = attributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MaxValue;
            paramDescription.Required = attributes?.OfType<ApiRequiredAttribute>().FirstOrDefault()?.Required ?? false;
            
            return paramDescription;
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var method = typeof(T).GetMethod(methodName);
            if (method == null || method?.GetCustomAttributes().OfType<ApiMethodAttribute>().FirstOrDefault() == null)
                return null;
            
            var res = new ApiMethodDescription
            {
                MethodDescription = new CommonDescription {
                    Name = methodName, 
                    Description =  GetApiMethodDescription(methodName)
                    
                },
                ParamDescriptions = method.GetParameters()?
                    .Select(param => GetApiMethodParamFullDescription(methodName, param.Name)).ToArray(),
                ReturnDescription = GetReturnDescription(method)
            };
            return res;
        }

        private static ApiParamDescription GetReturnDescription(MethodInfo method)
        {
            var invalidAtr = method?.ReturnParameter?.GetCustomAttributes().OfType<ApiIntValidationAttribute>().FirstOrDefault();
            var reqAtr = method?.ReturnParameter?.GetCustomAttributes().OfType<ApiRequiredAttribute>().FirstOrDefault();
            if (reqAtr == null && invalidAtr == null)
                return null;
            var returnDescription = new ApiParamDescription
            {
                MaxValue = invalidAtr?.MaxValue,
                MinValue = invalidAtr?.MinValue,
                Required = reqAtr?.Required ?? false
            };
            return returnDescription;
        }
    }
}