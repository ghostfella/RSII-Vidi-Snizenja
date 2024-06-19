using MediatR;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Application.Exceptions;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Result<GetUserByIdResponse>>
{
    private readonly IDbContext _context;

    public GetUserByIdRequestHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id.Equals(request.id) && !u.IsDeleted, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException($"{typeof(User).Name} not found!", typeof(User));
        }

        return Result.Ok(new GetUserByIdResponse(entity.Id, entity.UserName, entity.FirstName, entity.LastName, entity.Birthdate));
    }
}
