namespace Indego.Models
{
    public record UserSession
    (
        string Email,
        Guid ContextId,
        Guid UserId,
        string SerialNumber
    );
}