using AutoMapper;
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
        public CreatePackagesCommandHandler(
            IMapper mapper,
            ILogger logger,
            IPackagesRepository packagesDataRepository) : base(mapper,logger)
        {
            _packagesDataRepository = packagesDataRepository ?? throw new ArgumentNullException(nameof(packagesDataRepository));
        }

        public async Task<Packages> Handle(CreatePackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = _mapper.Map<Packages>(request);

            packages = _packagesDataRepository.Add(packages);
            
            if (packages==null)
            {
                throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"packages not found");

            }
            await _packagesDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Create Packages this is a information message...");

            return packages;
        }
    }
}
