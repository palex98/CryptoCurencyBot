﻿using System;
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
    }
}
