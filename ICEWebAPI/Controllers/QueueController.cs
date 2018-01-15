using ICEWebAPI.App_Start;
using ICEWebAPI.Models;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;

namespace ICEWebAPI.Controllers
{
    public class QueueController : ApiController
    {
        [HttpGet]
        [Route("~/readqueue")]
        public async Task<IEnumerable<Account>> Get()
        {
            return await Queue();
        }

        public async Task<IEnumerable<Account>> Queue()
        {
            var result = new List<Account>();
            try
            {
                //TokenProvider tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(ICEConfig.keyName, ICEConfig.accessKey);
                //MessagingFactory messagingFactory = MessagingFactory.Create(ICEConfig.baseAddress, tokenProvider);
                //var receiver = await messagingFactory.CreateMessageReceiverAsync(ICEConfig.queueName, ReceiveMode.ReceiveAndDelete);

                //while (true)
                //{
                //    var msg = await receiver.ReceiveAsync(TimeSpan.Zero);
                //    if (msg != null)
                //    {
                //        var serializer = new XmlSerializer(typeof(Account));
                //        var objresult = (Account)serializer.Deserialize(new StreamReader(msg.GetBody<Stream>()));
                //        result.Add(objresult);
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}

                for (int i = 0; i < 10; i++)
                {
                    result.Add(new Account() { Name = "Test Name ", Store_Type_Desc__c = "sample description", Description = "Sample Simple description" });
                }
            }
            catch (Exception)
            {
            }

            return result;
        }

    }
}
