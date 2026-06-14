using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Attendances.Commands
{
    public class CheckOutCommand : IRequest<IResult>
    {
        public int AttendanceId { get; set; }

        public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, IResult>
        {
            private readonly IAttendanceRepository _attendanceRepository;

            public CheckOutCommandHandler(IAttendanceRepository attendanceRepository)
            {
                _attendanceRepository = attendanceRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CheckOutCommand request, CancellationToken cancellationToken)
            {
                var attendance = await _attendanceRepository.GetAsync(a => a.Id == request.AttendanceId);
                if (attendance == null)
                    return new ErrorResult(Messages.Unknown);

                attendance.CheckOut = DateTime.Now;
                _attendanceRepository.Update(attendance);
                await _attendanceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
