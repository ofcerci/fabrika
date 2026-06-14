using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Lessons.Queries
{
    public class GetAllLessonsQuery : IRequest<IDataResult<IEnumerable<Lesson>>>
    {
        public class GetAllLessonsQueryHandler : IRequestHandler<GetAllLessonsQuery, IDataResult<IEnumerable<Lesson>>>
        {
            private readonly ILessonRepository _lessonRepository;

            public GetAllLessonsQueryHandler(ILessonRepository lessonRepository)
            {
                _lessonRepository = lessonRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Lesson>>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
            {
                var list = await _lessonRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Lesson>>(list);
            }
        }
    }
}
