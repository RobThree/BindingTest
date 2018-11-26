using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BindingTest.Binding
{
    public class MyModelBinder : ComplexTypeModelBinder
    {
        private readonly INumberFormatter _numberformatter;

        private static readonly ConcurrentDictionary<Type, Dictionary<string, FormatNumberAttribute>> _formatproperties = new ConcurrentDictionary<Type, Dictionary<string, FormatNumberAttribute>>();

        public MyModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders, INumberFormatter numberFormatter, ILoggerFactory loggerFactory)
            : base(propertyBinders, loggerFactory)
        {
            _numberformatter = numberFormatter;
        }

        protected override object CreateModel(ModelBindingContext bindingContext)
        {
            // Index and cache all properties having the FormatNumber Attribute
            _formatproperties.GetOrAdd(bindingContext.ModelType, (t) =>
            {
                return t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(FormatNumberAttribute))).ToDictionary(pi => pi.Name, pi => pi.GetCustomAttribute<FormatNumberAttribute>(), StringComparer.OrdinalIgnoreCase);
            });
            return base.CreateModel(bindingContext);
        }

        protected override Task BindProperty(ModelBindingContext bindingContext)
        {
            return base.BindProperty(bindingContext);
        }

        protected override void SetProperty(ModelBindingContext bindingContext, string modelName, ModelMetadata propertyMetadata, ModelBindingResult result)
        {
            if (_formatproperties.TryGetValue(bindingContext.ModelType, out var props) && props.TryGetValue(modelName, out var att))
            {
                // Do our formatting here
                var formatted = _numberformatter.FormatNumber(result.Model as string);
                base.SetProperty(bindingContext, modelName, propertyMetadata, ModelBindingResult.Success(formatted));
            } else
            {
                // Do nothing
                base.SetProperty(bindingContext, modelName, propertyMetadata, result);
            }
        }
    }
}
