using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DistSysAcwServer.DataAccess;
using DistSysAcwServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace DistSysAcwServer.Auth
{
    /// <summary>
    /// Authenticates clients by API Key
    /// </summary>
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>, IAuthenticationHandler
    {
        private Models.UserContext DbContext { get; set; }
        private IHttpContextAccessor HttpContextAccessor { get; set; }      
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly MyUserCRUD _myUserCRUDAccess = new MyUserCRUD();

        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            Models.UserContext dbContext,
            IHttpContextAccessor httpContextAccessor)
            : base(options, logger, encoder, clock) 
        {
            DbContext = dbContext;
            HttpContextAccessor = httpContextAccessor;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            #region Task5
            // TODO:  Find if a header ‘ApiKey’ exists, and if it does, check the database to determine if the given API Key is valid
            //        Then create the correct Claims, add these to a ClaimsIdentity, create a ClaimsPrincipal from the identity 
            //        Then use the Principal to generate a new AuthenticationTicket to return a Success AuthenticateResult
            #endregion

            if (!Request.Headers.TryGetValue("Api-Key", out var apiKeyHeaderValues))
            
            {
                return Task.FromResult(AuthenticateResult.Fail("ApiKey header is missing"));
            }

            string apiKey = apiKeyHeaderValues.FirstOrDefault();            
            bool isValidApiKey = _myUserCRUDAccess.CheckApi(apiKey);

            if (isValidApiKey)
            {
                User user = _myUserCRUDAccess.ReturnUser(apiKey);
               
                Claim[] claims = new Claim[]
                {
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Role, user.Role)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "ApiKey");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            else
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid ApiKey"));
            }
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes("Unauthorized. Check ApiKey in Header is correct.");
            Context.Response.StatusCode = 401;
            Context.Response.ContentType = "application/json";
            await Context.Response.Body.WriteAsync(messageBytes, 0, messageBytes.Length);
            await HttpContextAccessor.HttpContext.Response.CompleteAsync();
        }
    }
}