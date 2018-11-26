using BindingTest.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace BindingTest.Binding
{
    public class MyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var modelType = context.Metadata.ModelType;
            if (!typeof(MyComplexModel).IsAssignableFrom(modelType))
                return null;

            if (!context.Metadata.IsComplexType || context.Metadata.IsCollectionType)
                return null;

            var propertyBinders = context.Metadata.Properties
                .ToDictionary(modelProperty => modelProperty, context.CreateBinder);

            return new MyModelBinder(
                propertyBinders,
                (INumberFormatter)context.Services.GetService(typeof(INumberFormatter)),
                (ILoggerFactory)context.Services.GetService(typeof(ILoggerFactory))
            );
        }
    }
}
