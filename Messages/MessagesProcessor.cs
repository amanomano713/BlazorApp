using Azure.Messaging.ServiceBus;
using BlazorApp.Models;
using MassTransit;
using Microsoft.Azure.ServiceBus;
using System.Text.Json;

namespace BlazorApp.Messages
{
    public class MessagesProcessor : IMessagesProcessor
    {
        private readonly IConfiguration _configuration;
        public MessagesProcessor(IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public async Task<bool> SendRetiro(WithdrawalDTO withdrawalDTO)
        {
            var QueueAccessKey = _configuration["Masstransit:AzureServiceBusConnectionString"];

            ServiceBusConnectionStringBuilder conStr;
            
            QueueClient client;

            try
            {
                conStr = new ServiceBusConnectionStringBuilder(QueueAccessKey);
                
                client = new QueueClient(conStr);
                
                Message msg = new Message();
               
                string rawJsonRequest = JsonSerializer.Serialize(withdrawalDTO);
               
                msg.MessageId = rawJsonRequest;

                await client.SendAsync(msg).ConfigureAwait(false);
            }
            catch (Exception exe)
            {

            }
            return true;

        }

        public async Task<WithdrawalDTO> ServiceBusReceiver()
        {
            var QueueAccessKey = _configuration["Masstransit:AzureMessageConnectionString"];
            var QUEUE_NAME = _configuration["Masstransit:QUEUE_NAME"];
            var result = new WithdrawalDTO();

            try
            {
                ServiceBusClient _serviceBusClient = new ServiceBusClient(QueueAccessKey);

                var messageReceiver = _serviceBusClient.CreateReceiver(QUEUE_NAME);

                var message = await messageReceiver.ReceiveMessageAsync().ConfigureAwait(false);

                string MessageServiceBus = message.MessageId.ToString();

                result = JsonSerializer.Deserialize<WithdrawalDTO>(MessageServiceBus);
            }
            catch (Exception exe)
            {

            }
            return result;
        }

        
    }
}
