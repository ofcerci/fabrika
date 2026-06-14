using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Lessons.Commands
{
    public class CreateLessonCommand : IRequest<IResult>
    {
        public string Name { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }

        public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, IResult>
        {
            private readonly ILessonRepository _lessonRepository;

            public CreateLessonCommandHandler(ILessonRepository lessonRepository)
            {
                _lessonRepository = lessonRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
            {
                var lesson = new Lesson
                {
                    Name = request.Name,
                    TrainerId = request.TrainerId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Capacity = request.Capacity,
                    Location = request.Location,
                    Notes = request.Notes,
                    IsActive = true
                };

                _lessonRepository.Add(lesson);
                await _lessonRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
