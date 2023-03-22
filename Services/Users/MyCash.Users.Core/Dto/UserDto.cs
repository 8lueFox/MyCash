using Micro.Messaging.RabbitMQ;

namespace MyCash.Users.Core.Dto;

public class UserDto : BusPublishDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Package { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
