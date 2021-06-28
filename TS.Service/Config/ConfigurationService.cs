using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Entities.Config;
using TS.Infra.Repositories.Config;
using TS.Service.Base;

namespace TS.Service.Config
{
    public class ConfigurationService : IService
    {        
		private readonly ConfigurationRepository configParameterRepository;
        public ConfigurationService(ConfigurationRepository configParameterRepository)
        {
            this.configParameterRepository = configParameterRepository;
        }

        public async Task<IEnumerable<ConfigParameterEntity>> GetConfigParameter()
        {
            return await configParameterRepository.GetConfigParameter();
        }
    }
}
