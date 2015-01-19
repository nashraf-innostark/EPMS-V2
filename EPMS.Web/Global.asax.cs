using System.Configuration;
using System.IO;
using EPMS.WebBase;
using EPMS.WebBase.UnityConfiguration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Web.Http;
using UnityDependencyResolver = EPMS.WebBase.UnityConfiguration.UnityDependencyResolver;
using System;
using System.Web;
using System.Globalization;

namespace IdentitySample
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Private
        private static IUnityContainer container;
        /// <summary>
        /// Configure Logger
        /// </summary>
        private void ConfigureLogger()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());
        }
        /// <summary>
        /// Create the unity container
        /// </summary>
        private static IUnityContainer CreateUnityContainer()
        {
            container = UnityWebActivator.Container;
            RegisterTypes();

            return container;
        }
        /// <summary>
        /// Register types with the IoC
        /// </summary>
        private static void RegisterTypes()
        {
            TypeRegistrations.RegisterTypes(container);
            EPMS.Implementation.TypeRegistrations.RegisterType(container);

        }
        /// <summary>
        /// Register unity 
        /// </summary>
        private static void RegisterIoC()
        {
            if (container == null)
            {
                container = CreateUnityContainer();
            }
        }
        #endregion
        protected void Application_Start()
        {
            RegisterIoC();
            AreaRegistration.RegisterAllAreas();
            ConfigureLogger();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set MVC resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Set Web Api resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            ////Date Formatter
            //ModelBinders.Binders.Add(typeof(DateTime), new MyDateTimeModelBinder());
        }
        private void Session_Start(object sender, EventArgs e)
        {
            try
            {
                #region Visitor counter

                //int count = 0;
                //count = GetAndUpdateCounterValue();
                //Session["VisitorCounter"] = count;

                #endregion
            }
            catch (Exception ex)
            {
                
            }
            }

        private int GetAndUpdateCounterValue()
        {
            StreamReader stmReader;
            StreamWriter stmWriter;
            FileStream fileStream;
            string fileContents;
            int counter = 0;
            string filePath = Server.MapPath(ConfigurationManager.AppSettings["CounterFilePath"]);

            #region Read File
                if (File.Exists(filePath))
                {
                    stmReader = File.OpenText(filePath);
                    fileContents = stmReader.ReadLine();
                    if (fileContents != null)
                    {
                        counter = Convert.ToInt32(fileContents);
                    }
                    stmReader.Close();
                }
                else
                {
                    counter = 0;
                }
            #endregion

            counter++;
            #region Write File
                fileStream =new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.Write);
                stmWriter=new StreamWriter(fileStream);
                stmWriter.WriteLine(Convert.ToString(counter));
                stmWriter.Close();
            #endregion

            return counter;
        }
        protected void Application_BeginRequest()
        {
            //On each request, it will disable the creation of Cache for that request/page.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "dd/mm/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }
        //public class MyDateTimeModelBinder : DefaultModelBinder
        //{
        //    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //    {
        //        var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
        //        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        //        if (!string.IsNullOrEmpty(displayFormat) && value != null)
        //        {
        //            DateTime date;
        //            displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
        //            // use the format specified in the DisplayFormat attribute to parse the date
        //            if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        //            {
        //                return date;
        //            }
        //            else
        //            {
        //                bindingContext.ModelState.AddModelError(
        //                    bindingContext.ModelName,
        //                    string.Format("{0} is an invalid date format", value.AttemptedValue)
        //                );
        //            }
        //        }
        //        return base.BindModel(controllerContext, bindingContext);
        //    }
        //}
    }
}