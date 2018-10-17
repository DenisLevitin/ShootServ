using System;
using System.Globalization;
using System.Web.Mvc;

namespace ShootServ
{
    public class ModelDateBinder : DefaultModelBinder
    {
        private string _customFormat;

        public ModelDateBinder(string customFormat) : base()
        {
            _customFormat = customFormat;
        }
 
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            return DateTime.ParseExact(value.AttemptedValue, _customFormat, CultureInfo.InvariantCulture);
        }
    }
}