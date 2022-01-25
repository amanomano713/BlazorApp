using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using MediatR;

namespace BlazorApp.Handlers.User
{
    public class CreatePackagesCommandHandler : BaseCommandHandler<CreatePackagesCommandHandler>, IRequestHandler<CreatePackagesCommand, Packages>
    {
        private readonly IPackagesRepository _packagesDataRepository;
        public CreatePackagesCommandHandler(
            IMapper mapper,
            IPackagesRepository packagesDataRepository) : base(mapper)
        {
            _packagesDataRepository = packagesDataRepository ?? throw new ArgumentNullException(nameof(packagesDataRepository));
        }

        public async Task<Packages> Handle(CreatePackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = _mapper.Map<Packages>(request);

            _packagesDataRepository.Add(packages);

            await _packagesDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return packages;
        }
    }
}
