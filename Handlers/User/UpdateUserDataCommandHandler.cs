using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.DataAcess.Infraestructure.Repositories;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using MediatR;

namespace BlazorApp.Handlers.User
{
    public class UpdateUserDataCommandHandler : BaseCommandHandler<UpdateUserDataCommandHandler>, IRequestHandler<UpdateUserDataCommand, UserData>
    {
        private readonly IUserDataRepository _userDataRepository;

        public UpdateUserDataCommandHandler(
            IUserDataRepository userDataRepository,
            IMapper mapper, ILogger logger) : base( mapper, logger)
        {
            _userDataRepository = userDataRepository ?? throw new ArgumentNullException(nameof(userDataRepository));

        }

        public async Task<UserData> Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
        {

            var userdata = _mapper.Map<UserData>(request);

            await _userDataRepository.Update(userdata);

            await _userDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return userdata;
        }
    }
}
