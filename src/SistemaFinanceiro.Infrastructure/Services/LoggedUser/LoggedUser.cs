using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Security.Tokens;
using SistemaFinanceiro.Domain.Services.LoggerUser;
using SistemaFinanceiro.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SistemaFinanceiro.Infrastructure.Services.LoggerUser
{
    internal class LoggedUser : ILoggedUser
    {
        private readonly SistemaFinanceiroDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(SistemaFinanceiroDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }

        public async Task<User> Get()
        {
            string token = _tokenProvider.TokenOnRequest();
            var tokenHanlder = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHanlder.ReadJwtToken(token);
            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstAsync(x => x.UserIdentifier == Guid.Parse(identifier));
        }
    }
}
