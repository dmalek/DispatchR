using DispatchR.Contracts;

namespace sample.Domain.User.Update;

public class UserUpdate : IRequest<bool>
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
