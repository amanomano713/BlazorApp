using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using GOfit.MyGOfit.ExceptionMiddleware;
using GOfit.MyGOfit.ExceptionMiddleware.Enums;
using MediatR;

namespace BlazorApp.Handlers.User
{
    public class CreatUserDataCommandHandler : BaseCommandHandler<CreatUserDataCommandHandler>, IRequestHandler<CreateUserDataCommand, UserData>
    {
        private readonly IUserDataRepository _userDataRepository;

        public CreatUserDataCommandHandler(
            IUserDataRepository userDataRepository,
            IMapper mapper, ILogger<CreatUserDataCommandHandler> logger) : base( mapper, logger)
        {
            _userDataRepository = userDataRepository ?? throw new ArgumentNullException(nameof(userDataRepository));
        }

        public async Task<UserData> Handle(CreateUserDataCommand request, CancellationToken cancellationToken)
        {
            
            var userdata = _mapper.Map<UserData>(request);

            _userDataRepository.Add(userdata);

            if (userdata == null)
            {
                throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"packages not found");
            }

            await _userDataRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Create UserData this is a information message...");

            return userdata;
        }
    }
}
