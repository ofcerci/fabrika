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

namespace Business.Handlers.Attendances.Queries
{
    public class GetMemberAttendancesQuery : IRequest<IDataResult<IEnumerable<Attendance>>>
    {
        public int MemberId { get; set; }

        public class GetMemberAttendancesQueryHandler : IRequestHandler<GetMemberAttendancesQuery, IDataResult<IEnumerable<Attendance>>>
        {
            private readonly IAttendanceRepository _attendanceRepository;

            public GetMemberAttendancesQueryHandler(IAttendanceRepository attendanceRepository)
            {
                _attendanceRepository = attendanceRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Attendance>>> Handle(GetMemberAttendancesQuery request, CancellationToken cancellationToken)
            {
                var list = await _attendanceRepository.GetMemberAttendances(request.MemberId);
                return new SuccessDataResult<IEnumerable<Attendance>>(list);
            }
        }
    }
}
