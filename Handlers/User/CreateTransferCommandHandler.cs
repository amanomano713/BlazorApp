using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using GOfit.MyGOfit.ExceptionMiddleware;
using GOfit.MyGOfit.ExceptionMiddleware.Enums;
using MediatR;

namespace BlazorApp.Handlers.User
{
    public class CreateTransferCommandHandler : BaseCommandHandler<CreateTransferCommandHandler>, IRequestHandler<CreateTransferCommand, Transfer>
    {
        private readonly ITransferRepository _transferRepository;
        public CreateTransferCommandHandler(
            IMapper mapper,
            ILogger<CreateTransferCommandHandler> logger,
            ITransferRepository transferRepository) : base(mapper, logger)
        {
            _transferRepository = transferRepository ?? throw new ArgumentNullException(nameof(transferRepository));
        }

        public async Task<Transfer> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            var transfer = _mapper.Map<Transfer>(request);

            transfer = _transferRepository.Add(transfer);
            
            if (transfer==null)
            {
                throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"transfer not found");
            }

            await _transferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Create Transfer this is a information message...");

            return transfer;
        }
    }
}
