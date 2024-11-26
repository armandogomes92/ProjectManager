using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Report.Queries;

public class GetReportsFromTheLast30DaysHandler : CommandHandler<GetReportsFromTheLast30DaysCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IReportRepository _reportRepository;

    public GetReportsFromTheLast30DaysHandler(IUserRepository userRepository, IReportRepository reportRepository)
    {
        _userRepository = userRepository;
        _reportRepository = reportRepository;
    }

    public override async Task<Response> Handle(GetReportsFromTheLast30DaysCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(command.UserId);
        if (user == null)
        {
            return new Response { Content = Messages.UserNotFound };
        }
        else
        {
            if (user.Cargo != PositionEnum.Gerente)
            {
                return new Response { Content = Messages.PositionHasNoAccess };
            }
            return new Response { Content = await _reportRepository.GetUserPerformanceReportAsync() };
        }
    }
}