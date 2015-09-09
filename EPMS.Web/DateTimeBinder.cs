using System;
using System.Globalization;
using System.Web.Mvc;

namespace EPMS.Web
{
    public class DateTimeBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string date = controllerContext.HttpContext.Request[bindingContext.ModelName];
            DateTime dt = new DateTime();

            bool success = DateTime.TryParse(date, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out dt);
            if (success)
                return dt;
            return default(DateTime);
        }
    }
}