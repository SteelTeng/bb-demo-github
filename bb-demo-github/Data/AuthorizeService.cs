namespace bb_demo_github.Data
{
    public class AuthorizeService(UserService userService)
    {
        private readonly UserService _userService = userService;
        public async Task<bool> IsAuhorizeMenuAsync(int userId, string url) =>
            (await _userService.GetResourcesByUserIdAsync(userId))
            .Any(p => p.Value.Contains(url, StringComparison.OrdinalIgnoreCase));

        public bool IsAuhorizeMenu(int userId, string url) =>
            _userService.GetResourcesByUserId(userId)
            .Any(p => p.Value.Contains(url, StringComparison.OrdinalIgnoreCase));

    }
}
