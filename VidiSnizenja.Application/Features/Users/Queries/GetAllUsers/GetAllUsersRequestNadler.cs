using MediatR;
using VidiSnizenja.Application.Extensions;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Queries.GetAllUsers;

public sealed class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, Result<List<GetAllUsersResponse>>>
{
    private readonly IDbContext _context;

    public GetAllUsersRequestHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<GetAllUsersResponse>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .WhereIf(!string.IsNullOrWhiteSpace(request.firstName), u => u.FirstName.ToLower().Contains(request.firstName.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(request.lastName), u => u.LastName.ToLower().Contains(request.lastName.ToLower()))
            .Select(u => new GetAllUsersResponse(u.Id, u.UserName, u.FirstName, u.LastName, u.Birthdate))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Result.Ok(users);
    }
}
