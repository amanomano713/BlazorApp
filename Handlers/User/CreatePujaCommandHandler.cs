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
    public class CreatePujaCommandHandler : BaseCommandHandler<CreatePujaCommandHandler>, IRequestHandler<CreatePujaCommand, Puja>
    {
        private readonly IPujaRepository _PujaDataRepository;
        private readonly Context _context;
        public CreatePujaCommandHandler(
            IMapper mapper,
            ILogger<CreatePujaCommandHandler> logger,
             Context context,
            IPujaRepository PujaDataRepository) : base(mapper, logger)
        {
            _context = context;
            _PujaDataRepository = PujaDataRepository ?? throw new ArgumentNullException(nameof(PujaDataRepository));
        }

        public async Task<Puja> Handle(CreatePujaCommand request, CancellationToken cancellationToken)
        {
            var Puja = _mapper.Map<Puja>(request);

            //var movPackage = new MovPackage
            //{
            //    IdAfiliado = request.Id,
            //    DateCreated = DateTime.Now,
            //    MontoTransferido = 0,
            //    IdPackage = 0,
            //    CodPackage = "Retiro",
            //    Porcentaje = 0,
            //    MontoPackage = 0,
            //    Interes = 0,
            //    MontoRetiro = request.Monto
            //};

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Puja.Add(Puja);
                   //_context.MovPackage.Add(movPackage);
                    await _context.SaveEntitiesAsync(cancellationToken);
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"transfer not found");
                }

            }
            //_PujaDataRepository.Add(Puja);

            //if (Puja == null)
            //{
            //    throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"packages not found");
            //}

            //await _PujaDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Create Puja this is a information message...");

            return Puja;
        }
    }
}
