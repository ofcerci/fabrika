using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Fakes.Handlers.Lessons
{
    public class CreateLessonInternalCommand : IRequest<IResult>
    {
        public string Name { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }

        public class CreateLessonInternalCommandHandler : IRequestHandler<CreateLessonInternalCommand, IResult>
        {
            private readonly ILessonRepository _lessonRepository;

            public CreateLessonInternalCommandHandler(ILessonRepository lessonRepository)
            {
                _lessonRepository = lessonRepository;
            }

            [CacheRemoveAspect()]
            public async Task<IResult> Handle(CreateLessonInternalCommand request, CancellationToken cancellationToken)
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
