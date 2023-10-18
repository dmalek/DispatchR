using DispatchR;

namespace sample.Application.User.Get
{
    public class GetUserHandler : IRequestHandler<GetUser, GetUserResponse>
    {
        public async Task<GetUserResponse> HandleAsync(GetUser request, CancellationToken cancellationToken)
        {
            // do some work
            var user = new GetUserResponse()
            {
                Id = request.Id,
                FirstName = "John",
                LastName = "Doe"
            };

            return await Task.FromResult(user);
        }
    }
}
