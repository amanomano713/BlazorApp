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
    public class CreateWithdrawalCommandHandler : BaseCommandHandler<CreateWithdrawalCommandHandler>, IRequestHandler<CreateWithdrawalCommand, Withdrawal>
    {
        private readonly IWithdrawalRepository _withdrawalDataRepository;
        private readonly Context _context;
        public CreateWithdrawalCommandHandler(
            IMapper mapper,
            ILogger<CreateWithdrawalCommandHandler> logger,
             Context context,
            IWithdrawalRepository withdrawalDataRepository) : base(mapper, logger)
        {
            _context = context;
            _withdrawalDataRepository = withdrawalDataRepository ?? throw new ArgumentNullException(nameof(withdrawalDataRepository));
        }

        public async Task<Withdrawal> Handle(CreateWithdrawalCommand request, CancellationToken cancellationToken)
        {
            var withdrawal = _mapper.Map<Withdrawal>(request);

            var movPackage = new MovPackage
            {
                IdAfiliado = request.Id,
                DateCreated = DateTime.Now,
                MontoTransferido = 0,
                IdPackage = 0,
                CodPackage = "Retiro",
                Porcentaje = 0,
                MontoPackage = 0,
                Interes = 0,
                MontoRetiro = request.Monto
            };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Withdrawal.Add(withdrawal);
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
            //_withdrawalDataRepository.Add(withdrawal);

            //if (withdrawal == null)
            //{
            //    throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"packages not found");
            //}

            //await _withdrawalDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Create Withdrawal this is a information message...");

            return withdrawal;
        }
    }
}
