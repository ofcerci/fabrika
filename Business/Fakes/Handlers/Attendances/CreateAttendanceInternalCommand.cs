using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Fakes.Handlers.Attendances
{
    public class CreateAttendanceInternalCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Note { get; set; }

        public class CreateAttendanceInternalCommandHandler : IRequestHandler<CreateAttendanceInternalCommand, IResult>
        {
            private readonly IAttendanceRepository _attendanceRepository;

            public CreateAttendanceInternalCommandHandler(IAttendanceRepository attendanceRepository)
            {
                _attendanceRepository = attendanceRepository;
            }

            [CacheRemoveAspect()]
            public async Task<IResult> Handle(CreateAttendanceInternalCommand request, CancellationToken cancellationToken)
            {
                var attendance = new Attendance
                {
                    MemberId = request.MemberId,
                    CheckIn = request.CheckIn,
                    CheckOut = request.CheckOut,
                    Note = request.Note
                };

                _attendanceRepository.Add(attendance);
                await _attendanceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
