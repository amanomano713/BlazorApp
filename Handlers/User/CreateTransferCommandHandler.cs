using AutoMapper;
using BlazorApp.Data.EF;
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
        private readonly Context _context;
        public CreateTransferCommandHandler(
            IMapper mapper,
            ILogger<CreateTransferCommandHandler> logger,
            Context context,
            ITransferRepository transferRepository) : base(mapper, logger)
        {
            _context = context;
            _transferRepository = transferRepository ?? throw new ArgumentNullException(nameof(transferRepository));
        }

        public async Task<Transfer> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {

            var transfer = _mapper.Map<Transfer>(request);

            var movPackage = new MovPackage
            {
                IdAfiliado = request.Afiliado,
                DateCreated = DateTime.Now,
                MontoTransferido = request.Monto,
                IdPackage = 0,
                CodPackage = "Transfer",
                Porcentaje = 0,
                MontoPackage = 0,
                Interes = 0,    
                MontoRetiro = 0
            };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Transfer.Add(transfer);
                    _context.MovPackage.Add(movPackage);
                    await _context.SaveEntitiesAsync(cancellationToken);
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"transfer not found");
                }

            }

            _logger.LogInformation("Create transfer this is a information message...");

            return transfer;
        }
    }
}
