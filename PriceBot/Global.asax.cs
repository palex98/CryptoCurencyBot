using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;


namespace PriceBot
{
    public class MvcApplication : System.Web.HttpApplication
    {

        static public Telegram.Bot.TelegramBotClient Bot;
        public static long ChatId { set; get; } = 150945128;
        public static int MessageId { set; get; } = 1026;
        public static string key = "583592021:AAF8EzDHNKIEwgcBhJ4MwOWtMr6v7P3UlwA";

        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bot = new Telegram.Bot.TelegramBotClient(key);
            await Bot.SetWebhookAsync("https://pricebot2018.azurewebsites.net:443/api/message/update");
            await Bot.SendTextMessageAsync(ChatId, "START");

            while (true)
            {
                await Run(0);
            }

        }

        public static async Task Run(object obj)
        {
            if (ChatId != 0 & MessageId != 0)
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.exmo.com/v1/ticker/");

                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                var streamReader = new StreamReader(httpWebResponse.GetResponseStream());

                var result = streamReader.ReadToEnd();

                RootObject ans = JsonConvert.DeserializeObject<RootObject>(result);

                string text = "Buy: <b>" + ans.XRP_USD.buy_price + "</b>\n" + "Sell: <b>" + ans.XRP_USD.sell_price + "</b>\n\n" +
                    "BTC: <b>" + ans.BTC_USD.buy_price.Substring(0, 4) + "</b>\n\n" + "<i>" + DateTime.UtcNow.AddHours(3).ToLongTimeString() + "</i>";
                try
                {
                    await Bot.EditMessageTextAsync(ChatId, MessageId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
                }
                catch
                {
                    Thread.Sleep(60000);
                }
            }
            Thread.Sleep(2000);
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
