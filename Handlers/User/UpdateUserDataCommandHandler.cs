using AutoMapper;
using BlazorApp.Data.EF;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Handlers.User
{
    public class UpdateUserDataCommandHandler : BaseCommandHandler<UpdateUserDataCommandHandler>, IRequestHandler<UpdateUserDataCommand, UserData>
    {
        private readonly IUserDataRepository _userDataRepository;

        public UpdateUserDataCommandHandler(
            IUserDataRepository userDataRepository,
            IMapper mapper) : base( mapper)
        {
            _userDataRepository = userDataRepository ?? throw new ArgumentNullException(nameof(userDataRepository));
            //_context = context ?? throw new ArgumentNullException( nameof(context));
        }

        public async Task<UserData> Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
        {

            var userdata = _mapper.Map<UserData>(request);

            var result = _userDataRepository.Update(userdata);

            await _userDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return userdata;
        }
    }
}
