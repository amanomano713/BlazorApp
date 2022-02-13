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
    public class CreatePackagesCommandHandler : BaseCommandHandler<CreatePackagesCommandHandler>, IRequestHandler<CreatePackagesCommand, Packages>
    {
        private readonly IPackagesRepository _packagesDataRepository;
        private readonly Context _context;
        public CreatePackagesCommandHandler(
            IMapper mapper,
             Context context,
            ILogger<CreatePackagesCommandHandler> logger,
            IPackagesRepository packagesDataRepository) : base(mapper,logger)
        {
            _context = context; 
            _packagesDataRepository = packagesDataRepository ?? throw new ArgumentNullException(nameof(packagesDataRepository));
        }

        public async Task<Packages> Handle(CreatePackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = _mapper.Map<Packages>(request);

            var movPackage = new MovPackage
            {
                IdAfiliado = request.IdAfiliado,
                DateCreated = DateTime.Now,
                MontoTransferido = 0,
                IdPackage = 0,
                CodPackage = "Crypto",
                Porcentaje = 0,
                MontoPackage = request.Monto,
                Interes = 0,
                MontoRetiro = 0
            };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Packages.Add(packages);
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

            //packages = _packagesDataRepository.Add(packages);

            //if (packages==null)
            //{
            //    throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"packages not found");

            //}
            //await _packagesDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Create Packages this is a information message...");

            return packages;
        }
    }
}
