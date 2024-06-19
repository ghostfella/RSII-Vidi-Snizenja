namespace VidiSnizenja.Application.Features.Users.Queries.GetUserById;

public sealed record GetUserByIdResponse(string id, string username, string firstName, string lastName, DateTime birthdate);