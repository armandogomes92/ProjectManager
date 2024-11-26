using MediatR;
using ProjectManagerApi.Domain.Models.CommonResources;

namespace ProjectManagerApi.Application.Handlers.CommonResources;

public abstract class QueryHandler<TQuery> : IRequestHandler<TQuery, Response> where TQuery : Query
{
    public abstract Task<Response> Handle(TQuery query, CancellationToken cancellationToken);
}