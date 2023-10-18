using DispatchR;

namespace sample.Application.User.Update
{
    public class UserUpdateHandler : IRequestHandler<UserUpdate, bool>
    {
        public async Task<bool> HandleAsync(UserUpdate request, CancellationToken cancellationToken = default)
        {
            // do some work

            return await Task.FromResult(true);
        }
    }
}
