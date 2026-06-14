using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Lessons.Queries
{
    public class GetLessonsQuery : IRequest<IDataResult<IEnumerable<Lesson>>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class GetLessonsQueryHandler : IRequestHandler<GetLessonsQuery, IDataResult<IEnumerable<Lesson>>>
        {
            private readonly ILessonRepository _lessonRepository;

            public GetLessonsQueryHandler(ILessonRepository lessonRepository)
            {
                _lessonRepository = lessonRepository;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Lesson>>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
            {
                var list = await _lessonRepository.GetLessonsByDateRange(request.StartDate, request.EndDate);
                return new SuccessDataResult<IEnumerable<Lesson>>(list);
            }
        }
    }
}
