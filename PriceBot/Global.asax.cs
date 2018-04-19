using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace PriceBot
{
    public class MvcApplication : System.Web.HttpApplication
    {

        static public Telegram.Bot.TelegramBotClient Bot;
        string key = "583592021:AAF8EzDHNKIEwgcBhJ4MwOWtMr6v7P3UlwA";
        public static long ChatId { set; get; } = 0;


        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bot = new Telegram.Bot.TelegramBotClient(key);
            await Bot.SetWebhookAsync("https://http://pricebot2018.azurewebsites.net/:443/api/message/update");

            TimerCallback tm = new TimerCallback(Run);

            Timer timer = new Timer(tm, 0, 0, 20000);

        }

        public async void Run(object obj)
        {
            var httpWebRequest =  (HttpWebRequest)WebRequest.Create("https://api.exmo.com/v1/ticker/");

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            var streamReader = new StreamReader(httpWebResponse.GetResponseStream());

            var result = streamReader.ReadToEnd();

            RootObject ans = JsonConvert.DeserializeObject<RootObject>(result);

            if(ChatId != 0)
            {
                await Bot.SendTextMessageAsync(ChatId, ans.XRP_USD.buy_price);
                //wasf
                //534651
            }
            
        }

    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

}
