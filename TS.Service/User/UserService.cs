using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using TS.Domain.Entities;
using TS.Infra.Repositories;
using TS.Service.Base;
using TS.Service.Exceptions;
using TS_Security;
using TS_Security.Dtos;

namespace TS.Service
{
    public class UserService : IService
    {
        private readonly ClaimsPrincipal loggedUser;
        private readonly UserRepository userRepository;
        private readonly Encriptor encriptor;
        private readonly AuthenticationTokenProvider authenticationToken;

        public UserService(IPrincipal principal, UserRepository userRepository, Encriptor encriptor, AuthenticationTokenProvider authenticationToken)
        {
            loggedUser = (ClaimsPrincipal)principal;
            this.userRepository = userRepository;
            this.encriptor = encriptor;
            this.authenticationToken = authenticationToken;
        }

        public async Task<SignInResult> SignInAsync(string userName, string password)
        {
            password = encriptor.Encode(password);

            var user = await userRepository.GetUser(userName, password);

            if (user == null)
                throw new UserNotFoundException();

            var userSecurity = new UserSecurity
            {
                UserId = user.UserId,
                Username = user.UserName,
                UserFullName = user.UserFullName,
                UserLevel = user.UserLevel,
                IsEnabled = user.IsEnabled
            };

            AuthWebToken token = authenticationToken.CreateToken(userSecurity);

            return new SignInResult { 
                Success = true,
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
            };
        }
    }
}
