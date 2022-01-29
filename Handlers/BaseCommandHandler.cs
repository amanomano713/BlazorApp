using AutoMapper;

namespace BlazorApp.Handlers
{
    public class BaseCommandHandler<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;

        public BaseCommandHandler(
            IMapper mapper,
            ILogger<T> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }
    }
}
