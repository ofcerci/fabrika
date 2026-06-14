using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Trainers.Queries
{
    public class GetTrainerQuery : IRequest<IDataResult<Trainer>>
    {
        public int Id { get; set; }

        public class GetTrainerQueryHandler : IRequestHandler<GetTrainerQuery, IDataResult<Trainer>>
        {
            private readonly ITrainerRepository _trainerRepository;

            public GetTrainerQueryHandler(ITrainerRepository trainerRepository)
            {
                _trainerRepository = trainerRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Trainer>> Handle(GetTrainerQuery request, CancellationToken cancellationToken)
            {
                var trainer = await _trainerRepository.GetAsync(t => t.Id == request.Id);
                return new SuccessDataResult<Trainer>(trainer);
            }
        }
    }
}
