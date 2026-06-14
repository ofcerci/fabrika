using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Trainers.Commands
{
    public class DeleteTrainerCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteTrainerCommandHandler : IRequestHandler<DeleteTrainerCommand, IResult>
        {
            private readonly ITrainerRepository _trainerRepository;

            public DeleteTrainerCommandHandler(ITrainerRepository trainerRepository)
            {
                _trainerRepository = trainerRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteTrainerCommand request, CancellationToken cancellationToken)
            {
                var trainer = await _trainerRepository.GetAsync(t => t.Id == request.Id);
                if (trainer == null)
                    return new ErrorResult(Messages.Unknown);

                _trainerRepository.Delete(trainer);
                await _trainerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
