using MediatR;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Commands.UpdateUser;

public sealed class UserUpdateCommandHandler : IRequestHandler<UpdateUserCommand, Result<UpdateUserCommandResponse>>
{
    private readonly IDbContext _context;
    private readonly ICurrentUser _currentUser;

    public UserUpdateCommandHandler(IDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<UpdateUserCommandResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        if (request.id != _currentUser.Id)
        {
            return Result.Fail<UpdateUserCommandResponse>("User not found!");
        }

        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);

        if (user is null)
        {
            return Result.Fail<UpdateUserCommandResponse>($"User with id: '{request.id}', not found!");
        }

        user.FirstName = request.firstName;
        user.LastName = request.lastName;
        user.Birthdate = request.birthdate;
        user.Email = request.email;
        user.NormalizedEmail = request.email.ToUpper();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok(new UpdateUserCommandResponse(user.Id, user.UserName));
    }
}
