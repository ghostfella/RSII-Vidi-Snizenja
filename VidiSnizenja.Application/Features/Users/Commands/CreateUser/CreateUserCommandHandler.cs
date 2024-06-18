using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserCommandResponse>>
{
    private readonly IVidiSnizenjaDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly ICurrentUser _currentUser;

    public CreateUserCommandHandler(IVidiSnizenjaDbContext context, UserManager<User> userManager, ICurrentUser currentUser)
    {
        _context = context;
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public async Task<Result<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        if (request.password != request.confirmPassword)
        {
            return Result.Fail<CreateUserCommandResponse>("Passwords doesn`t match!");
        }

        var user = new User()
        {
            FirstName = request.firstName,
            LastName = request.lastName,
            Birthdate = request.Birthdate,
            UserName = request.username,
            Email = request.email,
            CreatedBy = _currentUser.Id,
        };

        var userCreationResult = await _userManager.CreateAsync(user, request.password);

        if (!userCreationResult.Succeeded)
        {
            return Result.Fail<CreateUserCommandResponse>("Failed to insert user!");
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, AppRoles.USER);

        if (!addToRoleResult.Succeeded)
        {
            return Result.Fail<CreateUserCommandResponse>("Failed to assign role!");
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateUserCommandResponse(user.Id, user.UserName), "User successfully added!");
    }
}
