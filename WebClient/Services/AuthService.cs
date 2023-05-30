using Microsoft.AspNetCore.Components;

namespace WebClient.Services
{
    public class AuthService
    {
        private readonly NavigationManager _navigationManager;
        private readonly ApiConfiguration _configuration;

        public AuthService(NavigationManager navigationManager, ApiConfiguration configuration)
        {
            _navigationManager = navigationManager;
            _configuration = configuration;
        }

        public void HandleUnauthorized()
        {
            // Get the current URL as the return URL
            var returnUrl = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            
            // Redirect to the authentication URL
            _navigationManager.NavigateTo($"{_configuration.BaseAddress}/login?returnUrl={returnUrl}");
        }
    }
}