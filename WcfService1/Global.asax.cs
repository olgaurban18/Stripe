using System;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using Stripe;

namespace WcfService1
{
    public class Global : System.Web.HttpApplication
    {
        static IWindsorContainer container;

        protected void Application_Start(Object sender, EventArgs e)
        {
            StripeConfiguration.SetApiKey(AppSettingsHelper.GetSettingValue(Constants.StripeApiKey));

            container = new WindsorContainer();
            container.AddFacility<WcfFacility>();
            container.Install(new WindsorInstaller());
        }
    }
}