using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using MediatR;

namespace BlazorApp.Handlers.User
{
    public class CreatUserDataCommandHandler : BaseCommandHandler<CreatUserDataCommandHandler>, IRequestHandler<CreateUserDataCommand, UserData>
    {
        private readonly IUserDataRepository _userDataRepository;

        public CreatUserDataCommandHandler(
            IUserDataRepository userDataRepository,
            IMapper mapper) : base( mapper)
        {
            _userDataRepository = userDataRepository ?? throw new ArgumentNullException(nameof(userDataRepository));
        }

        public async Task<UserData> Handle(CreateUserDataCommand request, CancellationToken cancellationToken)
        {
            
            var userdata = _mapper.Map<UserData>(request);

            _userDataRepository.Add(userdata);

            await _userDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return userdata;
        }
    }
}
