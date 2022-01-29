using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using MediatR;

namespace BlazorApp.Handlers.User
{
    public class CreateWithdrawalCommandHandler : BaseCommandHandler<CreateWithdrawalCommandHandler>, IRequestHandler<CreateWithdrawalCommand, Withdrawal>
    {
        private readonly IWithdrawalRepository _withdrawalDataRepository;
        public CreateWithdrawalCommandHandler(
            IMapper mapper,
            ILogger<CreateWithdrawalCommandHandler> logger,
            IWithdrawalRepository withdrawalDataRepository) : base(mapper, logger)
        {
            _withdrawalDataRepository = withdrawalDataRepository ?? throw new ArgumentNullException(nameof(withdrawalDataRepository));
        }

        public async Task<Withdrawal> Handle(CreateWithdrawalCommand request, CancellationToken cancellationToken)
        {
            var withdrawal = _mapper.Map<Withdrawal>(request);

            _withdrawalDataRepository.Add(withdrawal);

            await _withdrawalDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return withdrawal;
        }
    }
}
