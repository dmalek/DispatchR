using DispatchR.Contracts;

namespace sample.Application.User.Get
{
    public class GetUser : IRequest<GetUserResponse>
    {
        public string Id { get; set; } = string.Empty;
    }
}
