using Dapper;
using System.Linq;
using System.Threading.Tasks;
using TS.Domain.Entities;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories
{
    public class UserRepository : IRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUser(string userName, string password)
        {
            var user = (await context
                .Connection
                .QueryAsync<User>(@"
                    select * from [dbo].[User]
                    where (UserName = @userName or userMail = @userName) and userPass = @password
                    ", new { userName, password })
                ).FirstOrDefault();

            return user;
        }
    }
}
