using System;
using System.Threading.Tasks;
using Business.Services.Authentication.Model;
using Core.Utilities.Results;

namespace Business.Services.Authentication
{
    public class AgentAuthenticationProvider : IAuthenticationProvider
    {
        public Task<LoginUserResult> Login(LoginUserCommand command)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDataResult<FabrikaToken>> Verify(VerifyOtpCommand command)
        {
            throw new NotImplementedException();
        }
    }
}