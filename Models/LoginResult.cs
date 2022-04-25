namespace Indego.Models;
public record LoginResult
(
    bool Successful,
    string? Error,
    Guid? ContextId
);