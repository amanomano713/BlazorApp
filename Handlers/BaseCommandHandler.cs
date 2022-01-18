using AutoMapper;

namespace BlazorApp.Handlers
{
    public class BaseCommandHandler<T>
    {

        protected readonly IMapper _mapper;

        public BaseCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
