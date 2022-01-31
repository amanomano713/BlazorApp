using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using GOfit.MyGOfit.ExceptionMiddleware;
using GOfit.MyGOfit.ExceptionMiddleware.Enums;
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

            if (withdrawal == null)
            {
                throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"packages not found");
            }

            await _withdrawalDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Create Withdrawal this is a information message...");

            return withdrawal;
        }
    }
}
