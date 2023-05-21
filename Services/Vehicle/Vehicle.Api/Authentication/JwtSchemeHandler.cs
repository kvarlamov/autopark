using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AutoPark.Api.Authentication
{
    public class JwtSchemeHandler : AuthenticationHandler<JwtSchemeOptions>
    {
        private readonly IOptionsMonitor<JwtSchemeOptions> _options;
        private readonly ILoggerFactory _logger;
        private readonly UrlEncoder _encoder;
        private readonly ISystemClock _clock;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //todo - убрать в JwtSchemeOptions!!! НЕБЕЗОПАСНО
        private const string SecretKey = "this is my custom Secret key for authentication";

        public JwtSchemeHandler(
            IOptionsMonitor<JwtSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor) : base(options, logger, encoder, clock)
        {
            _options = options;
            _logger = logger;
            _encoder = encoder;
            _clock = clock;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var tokenPresented = _httpContextAccessor
                .HttpContext
                .Request
                .Headers
                .TryGetValue("Authorization", out var token);

            if (!tokenPresented)
                return Task.FromResult(AuthenticateResult.Fail("There is no token presented"));
            
            token = token.ToString().Replace("Bearer ", string.Empty);

            if (!IsTokenValid(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is not valid"));
            }
            
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // JWT токен может содержать в себе любую информацию, которую мы в него положили при формировании.
            // Забираем данные пользователя из токена и аутентифицируем пользователя.
            var claims = jwtToken.Claims;

            var identity = new ClaimsIdentity(claims, Scheme.Name);

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        
        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is no expiration in the generated token
                ValidateAudience = true, // Because there is no audiance in the generated token
                ValidateIssuer = true,   // Because there is no issuer in the generated token
                ValidIssuer = "autopark",
                ValidAudience = "test",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))// The same key as the one that generate the token
            };
        }
        
        private bool IsTokenValid(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(token, GetValidationParameters(), out var f);
            }
            catch (Exception e)
            {
                return false;
            }
            
            return true;
        }
    }
}