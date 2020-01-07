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
        public static RootObject ans;
        static public Telegram.Bot.TelegramBotClient Bot;
        public static long ChatId { set; get; } = 150945128;
        public static int MessageId { set; get; } = 403911;
        public static string key = "583592021:AAF8EzDHNKIEwgcBhJ4MwOWtMr6v7P3UlwA";

        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bot = new Telegram.Bot.TelegramBotClient(key);
            await Bot.SetWebhookAsync("https://cryptocurencybot.azurewebsites.net");
            var nn = await Bot.SendTextMessageAsync(ChatId, "START");

            while (true)
            {
                try
                {
                    await Run(0);
                }
                catch(Exception ex)
                {
                    Thread.Sleep(5000);
                }

            }

        }

        public static async Task Run(object obj)
        {
            if (ChatId != 0 & MessageId != 0)
            {

                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.exmo.com/v1/ticker/");

                    var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    var streamReader = new StreamReader(httpWebResponse.GetResponseStream());

                    var result = streamReader.ReadToEnd();

                    ans = JsonConvert.DeserializeObject<RootObject>(result);
                }
                catch
                {
                    return;
                }

                string text = "BTC:\nBuy: <b>" + ans.BTC_USD.buy_price + "</b>\n" + "Sell: <b>" + ans.BTC_USD.sell_price + "</b>\n\n" +
                    "ETH:\nBuy: <b>" + ans.ETH_USD.buy_price + "</b>\n" + "Sell: <b>" + ans.ETH_USD.sell_price + "</b>\n\n" +
                    "XRP:\nBuy: <b>" + ans.XRP_USD.buy_price + "</b>\n" + "Sell: <b>" + ans.XRP_USD.sell_price + "</b>\n\n" +
                    "TRX:\nBuy: <b>" + ans.TRX_USD.buy_price + "</b>\n" + "Sell: <b>" + ans.TRX_USD.sell_price + "</b>\n\n" +
                    "<i>" + DateTime.UtcNow.AddHours(2).ToLongTimeString() + "</i>";



                try
                {
                    await Bot.EditMessageTextAsync(ChatId, MessageId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
                }
                catch(Exception ex)
                {
                    Thread.Sleep(5000);
                    return;
                }
            }
            Thread.Sleep(5000);
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
