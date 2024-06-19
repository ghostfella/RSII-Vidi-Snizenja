using MediatR;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Commands.DeleteUser
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<DeleteUserCommandResponse>>
    {
        private readonly IDbContext _context;
        private readonly ICurrentUser _currentUser;

        public DeleteUserCommandHandler(IDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Result<DeleteUserCommandResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);

            if (user is null)
            {
                return Result.Fail<DeleteUserCommandResponse>("User not found!");
            }

            if (!_currentUser.Roles.Any(r => r == AppRoles.ADMIN) || user.Id != request.id)
            {
                return Result.Fail<DeleteUserCommandResponse>("Permission denied!");
            }

            // TO DO: Add SaveChangesInterceptor and use Remove method
            user.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<DeleteUserCommandResponse>(new DeleteUserCommandResponse(user.UserName));
        }
    }
}
