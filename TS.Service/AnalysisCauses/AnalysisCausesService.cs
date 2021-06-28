using System.Collections.Generic;
using System.Threading.Tasks;
using TS.Domain.Dtos.AnalysisCause;
using TS.Domain.Entities.AnalysisCause;
using TS.Infra.Repositories.AnalysisCause;
using TS.Service.Base;

namespace TS.Service.AnalysisCause
{
    public class AnalysisCausesService : IService
    {
        private readonly AnalysisCauseRepository analisysCausesRepository;

        public AnalysisCausesService(AnalysisCauseRepository analisysCausesRepository)
        {
            this.analisysCausesRepository = analisysCausesRepository;
        }

        public async Task<AnalysisOfCauseDto> GetAnalysisCausesWhy(int eventId)
        {
            return await analisysCausesRepository.GetAnalysisCausesWhy(eventId);
        }

        public async Task<IEnumerable<AnalysisCauseWhySubjectEntity>> GetAnalysisCausesWhySubject()
        {
            return await analisysCausesRepository.GetAnalysisCausesWhySubject();
        }

        public async Task<AnalysisCauseResponse> IncludeAnalysisCausesWhy(AnalisysOfCauseRequestDto command)
        {
            return await analisysCausesRepository.IncludeAnalysisCausesWhy(command);
        }
    }
}
