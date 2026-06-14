using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Attendances.Commands
{
    public class CreateAttendanceCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public string Note { get; set; }

        public class CreateAttendanceCommandHandler : IRequestHandler<CreateAttendanceCommand, IResult>
        {
            private readonly IAttendanceRepository _attendanceRepository;

            public CreateAttendanceCommandHandler(IAttendanceRepository attendanceRepository)
            {
                _attendanceRepository = attendanceRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
            {
                var attendance = new Attendance
                {
                    MemberId = request.MemberId,
                    CheckIn = DateTime.Now,
                    Note = request.Note
                };

                _attendanceRepository.Add(attendance);
                await _attendanceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
