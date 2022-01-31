using BlazorApp.Models;

namespace BlazorApp.Messages
{
    public interface IMessagesProcessor
    {

        public Task<bool> SendRetiro(WithdrawalDTO withdrawalDTO);

        public Task<WithdrawalDTO> ServiceBusReceiver();

    }

}
