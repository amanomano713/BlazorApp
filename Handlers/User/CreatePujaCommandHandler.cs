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
    public class CreatePujaCommandHandler : BaseCommandHandler<CreatePujaCommandHandler>, IRequestHandler<CreatePujaCommand, Puja>
    {
        private readonly IPujaRepository _PujaDataRepository;
        private readonly Context _context;
        public CreatePujaCommandHandler(
            IMapper mapper,
            ILogger<CreatePujaCommandHandler> logger,
             Context context,
            IPujaRepository PujaDataRepository) : base(mapper, logger)
        {
            _context = context;
            _PujaDataRepository = PujaDataRepository ?? throw new ArgumentNullException(nameof(PujaDataRepository));
        }

        public async Task<Puja> Handle(CreatePujaCommand request, CancellationToken cancellationToken)
        {
            var Puja = _mapper.Map<Puja>(request);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Puja.Add(Puja);
                    await _context.SaveEntitiesAsync(cancellationToken);
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new MyGOfitException(ExceptionType.Unknown, ExceptionRepository.NotFound, ExceptionEntity.Unknown, $"transfer not found");
                }
            }
            
            _logger.LogInformation("Create Puja this is a information message...");

            return Puja;
        }
    }
}
