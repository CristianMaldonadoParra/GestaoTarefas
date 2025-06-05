using System.Reflection;
using Application.Dto.Helper;
using FluentValidation.Results;

namespace Application.Dto.Extension
{
    public static class DtoExtensions
    {
        private static TModel? ToModel<TDto, TModel>(this TDto dto) where TDto : class
                                                                    where TModel : class, new()
        {
            if (dto == null) return null;

            var model = new TModel();
            var dtoProps = typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProps = typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProp in dtoProps)
            {
                var modelProp = Array.Find(modelProps, p => p.Name == dtoProp.Name && p.CanWrite);
                if (modelProp != null)
                {
                    // Se for uma lista de objetos filho
                    if (typeof(System.Collections.IEnumerable).IsAssignableFrom(dtoProp.PropertyType) &&
                        dtoProp.PropertyType.IsGenericType &&
                        dtoProp.PropertyType.GetGenericArguments()[0].Name.EndsWith("Dto"))
                    {
                        var dtoList = dtoProp.GetValue(dto) as System.Collections.IEnumerable;
                        var modelListType = typeof(List<>).MakeGenericType(modelProp.PropertyType.GetGenericArguments()[0]);
                        var modelList = (System.Collections.IList)Activator.CreateInstance(modelListType)!;

                        foreach (var childDto in dtoList!)
                        {
                            var childModel = childDto
                                .GetType()
                                .GetMethod("ToModel")
                                ?.MakeGenericMethod(
                                    childDto.GetType(),
                                    modelProp.PropertyType.GetGenericArguments()[0])
                                .Invoke(childDto, new object[] { childDto });

                            modelList.Add(childModel);
                        }
                        modelProp.SetValue(model, modelList);
                    }
                    // Propriedade simples
                    else if (modelProp.PropertyType == dtoProp.PropertyType)
                    {
                        var value = dtoProp.GetValue(dto);
                        modelProp.SetValue(model, value);
                    }
                    // Objeto filho simples
                    else if (dtoProp.PropertyType.Name.EndsWith("Dto") && modelProp.PropertyType.Name.EndsWith("Model"))
                    {
                        var childDto = dtoProp.GetValue(dto);
                        if (childDto != null)
                        {
                            var childModel = childDto
                                .GetType()
                                .GetMethod("ToModel")
                                ?.MakeGenericMethod(dtoProp.PropertyType, modelProp.PropertyType)
                                .Invoke(childDto, new object[] { childDto });
                            modelProp.SetValue(model, childModel);
                        }
                    }
                }
            }

            return model;
        }


        public static ModelResult<TModel> ToModelResult<TDto, TModel>(this TDto? dto, ValidationResult? validationResult = null) where TDto : class
                                                                                                                                 where TModel : class, new()
        {
            var result = new ModelResult<TModel>();

            if (dto != null)
                result.Model = dto.ToModel<TDto, TModel>();

            if (validationResult != null && validationResult.Errors != null)
            {
                foreach (var error in validationResult.Errors)
                    result.Errors.Add(error.ErrorMessage);
            }

            return result;
        }
    }
}
