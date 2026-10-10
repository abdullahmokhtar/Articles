using Articles.Abstractions;
using MediatR;

namespace Submission.Application.Features.CreateArticle;

internal class CreateAtricleCommandHandler : IRequestHandler<CreateAtricleCommand, IdResponse>
{
    public Task<IdResponse> Handle(CreateAtricleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
