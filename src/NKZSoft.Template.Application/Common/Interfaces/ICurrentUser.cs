namespace NKZSoft.Template.Application.Common.Interfaces;

public interface ICurrentUser
{
    long? Id { get; }

    string? FirstName { get; }

    string? LastName { get; }

    string? MiddleName { get; }
}
