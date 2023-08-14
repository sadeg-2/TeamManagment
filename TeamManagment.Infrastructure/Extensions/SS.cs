﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Task = System.Threading.Tasks.Task;

namespace TeamManagment.Infrastructure.Extensions
{
    public class HtmlEncodedModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None)
            {
                string value = valueProviderResult.FirstValue;
                string htmlEncodedValue = System.Web.HttpUtility.HtmlEncode(value);
                bindingContext.Result = ModelBindingResult.Success(htmlEncodedValue);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }

}
