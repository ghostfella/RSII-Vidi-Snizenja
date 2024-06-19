namespace VidiSnizenja.Application.Features.Users.Queries.GetAllUsers;

public sealed record GetAllUsersResponse(string id, string username, string firstName, string lastName, DateTime birthdate);