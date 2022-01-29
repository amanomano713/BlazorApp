using AutoMapper;

namespace BlazorApp.Handlers
{
    public class BaseCommandHandler<T>
    {

        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        public BaseCommandHandler(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}
