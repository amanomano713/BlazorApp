using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using MediatR;

namespace BlazorApp.Handlers.User
{
    public class CreateTransferCommandHandler : BaseCommandHandler<CreateTransferCommandHandler>, IRequestHandler<CreateTransferCommand, Transfer>
    {
        private readonly ITransferRepository _transferRepository;
        public CreateTransferCommandHandler(
            IMapper mapper,
            ITransferRepository transferRepository) : base(mapper)
        {
            _transferRepository = transferRepository ?? throw new ArgumentNullException(nameof(transferRepository));
        }

        public async Task<Transfer> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            var transfer = _mapper.Map<Transfer>(request);

            _transferRepository.Add(transfer);

            await _transferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return transfer;
        }
    }
}
