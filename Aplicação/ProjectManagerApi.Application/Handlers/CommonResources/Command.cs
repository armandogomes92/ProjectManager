using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using ProjectManagerApi.Domain.Models.CommonResources;

namespace ProjectManagerApi.Application.Handlers.CommonResources
{
    [ExcludeFromCodeCoverage]
    public class Command : IRequest<Response>, IBaseRequest
    {
        protected long Timestamp { get; }

        protected string? TraceId { get; }

        protected string? ParentId { get; }

        protected Command()
        {
            Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            TraceId = Activity.Current?.Id;
            ParentId = Activity.Current?.ParentId;
        }
    }
}