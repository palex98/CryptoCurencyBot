using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace PriceBot.Controllers
{
    public class MessageController : ApiController
    {

        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {

            /*if (update.Message.Text == "/xrp")
            {
                MvcApplication.ChatId = update.Message.Chat.Id;
                var msg = await MvcApplication.Bot.SendTextMessageAsync(MvcApplication.ChatId, "hello");
                MvcApplication.MessageId = msg.MessageId;
                await MvcApplication.Bot.SendTextMessageAsync(MvcApplication.ChatId, msg.MessageId.ToString());
            }*/

            return Ok();
        }

        /*public static async void Run(object obj)
        {
            work = true;
            while (work)
            {
                if (MvcApplication.ChatId != 0 & MvcApplication.MessageId > 0)
                {

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.exmo.com/v1/ticker/");

                    var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    var streamReader = new StreamReader(httpWebResponse.GetResponseStream());

                    var result = streamReader.ReadToEnd();

                    RootObject ans = JsonConvert.DeserializeObject<RootObject>(result);

                    await MvcApplication.Bot.EditMessageTextAsync(MvcApplication.ChatId, MvcApplication.MessageId, ans.XRP_USD.buy_price);
                }
                Thread.Sleep(1000);
            }*/
    }
}
