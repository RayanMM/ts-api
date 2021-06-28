using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Entities.Config;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.Config
{
    public class ConfigurationRepository : IRepository
    {
        private readonly DbContext context;

        public ConfigurationRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ConfigParameterEntity>> GetConfigParameter()
        {
            return await context.Connection.QueryAsync<ConfigParameterEntity>("SELECT ConfParameterValueId, ConfParameterValueLabel FROM  ConfParameter WHERE ConfParameterGrouper = 3 AND IsActive = 1");
        }
    }
}




