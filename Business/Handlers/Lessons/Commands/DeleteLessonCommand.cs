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

namespace Business.Handlers.Lessons.Commands
{
    public class DeleteLessonCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, IResult>
        {
            private readonly ILessonRepository _lessonRepository;

            public DeleteLessonCommandHandler(ILessonRepository lessonRepository)
            {
                _lessonRepository = lessonRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
            {
                var lesson = await _lessonRepository.GetAsync(l => l.Id == request.Id);
                if (lesson == null)
                    return new ErrorResult(Messages.Unknown);

                _lessonRepository.Delete(lesson);
                await _lessonRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
