using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Trainers.Queries
{
    public class GetTrainersQuery : IRequest<IDataResult<IEnumerable<Trainer>>>
    {
        public class GetTrainersQueryHandler : IRequestHandler<GetTrainersQuery, IDataResult<IEnumerable<Trainer>>>
        {
            private readonly ITrainerRepository _trainerRepository;

            public GetTrainersQueryHandler(ITrainerRepository trainerRepository)
            {
                _trainerRepository = trainerRepository;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Trainer>>> Handle(GetTrainersQuery request, CancellationToken cancellationToken)
            {
                var list = await _trainerRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Trainer>>(list);
            }
        }
    }
}
