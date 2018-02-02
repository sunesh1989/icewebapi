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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ICEWebAPI.Controllers
{
    public class QueueController : ApiController
    {
        [HttpGet]
        [Route("~/readqueue")]
        public async Task<IEnumerable<AccountNew>> Get()
        {
            return await Queue();
        }

        [HttpPost]
        [Route("~/pushsms")]
        public IHttpActionResult sendsms(IEnumerable<TwilioSMS> list)
        {
            try
            {
                foreach (var item in list)
                {
                     string accountSid = "AC9cedced3ea42a41190c724959bf10823";
                     string authToken = "b04a68a51793944b9265849e18037dff";
                    TwilioClient.Init(accountSid, authToken);

                    var to = new PhoneNumber("+919895814621");
                    var message = MessageResource.Create(
                    to,
                    from: new PhoneNumber("+13023874116"),
                    body: "This is the ship that made the Kessel Run in fourteen parsecs dd?");
                }

            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok();
        }

        public async Task<IEnumerable<AccountNew>> Queue()
        {
            var result = new List<AccountNew>();
            try
            {
                TokenProvider tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(ICEConfig.keyName, ICEConfig.accessKey);
                MessagingFactory messagingFactory = MessagingFactory.Create(ICEConfig.baseAddress, tokenProvider);
                var receiver = await messagingFactory.CreateMessageReceiverAsync(ICEConfig.queueName, ReceiveMode.ReceiveAndDelete);

                while (true)
                {
                    var msg = await receiver.ReceiveAsync(TimeSpan.Zero);
                    if (msg != null)
                    {
                        var serializer = new XmlSerializer(typeof(Account));
                        var objresult = (Account)serializer.Deserialize(new StreamReader(msg.GetBody<Stream>()));
                        if (objresult!=null)
                        {
                            result.Add(new AccountNew()
                            {
                                Name = objresult.Name,
                                Phone = objresult.Phone,
                                Website = objresult.Website,
                                SicDesc = objresult.SicDesc,
                                Reference_Number__c = objresult.Reference_Number__c,
                                Fax_Number__c = objresult.Fax_Number__c,
                                CGO_Email__c = objresult.CGO_Email__c,
                                Store_Type_Desc__c = objresult.Store_Type_Desc__c,
                                Opening_Hours_Main__c = objresult.Opening_Hours_Main__c,
                                Store_Manager__c = objresult.Store_Manager__c,
                                Store_Manager_Email__c = objresult.Store_Manager_Email__c,
                                Area_Manager_Email__c = objresult.Area_Manager_Email__c,
                                Area_Name__c = objresult.Area_Name__c,
                                Store_Manager_Contact__c = objresult.Store_Manager_Contact__c,
                                Area_Manager_Contact__c = objresult.Area_Manager_Contact__c,
                            });
                        }
                       
                    }
                    else
                    {
                        break;
                    }
                }
               
            }
            catch (Exception)
            {
            }

            return result;
        }

    }

    public class AccountNew
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string SicDesc { get; set; }
        public string Reference_Number__c { get; set; }
        public string Fax_Number__c { get; set; }
        public string CGO_Email__c { get; set; }
        public string Store_Type_Desc__c { get; set; }
        public string Opening_Hours_Main__c { get; set; }
        public string Store_Manager__c { get; set; }
        public string Store_Manager_Email__c { get; set; }
        public string Area_Manager_Email__c { get; set; }
        public string Area_Name__c { get; set; }
        public string Store_Manager_Contact__c { get; set; }
        public string Area_Manager_Contact__c { get; set; }
    }
   public class TwilioSMS
    {
        public string AuthToken { get; set; }
        public string MessageBody { get; set; }
        public string ToPhone { get; set; }
        public string FromPhone { get; set; }
    }
}

