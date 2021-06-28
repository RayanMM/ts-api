using System.Collections.Generic;
using System.Threading.Tasks;
using TS.Domain.Dtos.Investigation;
using TS.Domain.Entities.Investigation;
using TS.Infra.Repositories.Investigation;
using TS.Service.Base;

namespace TS.Service.Investigation
{
    public class InvestigationService : IService
    {
        private readonly InvestigationRepository investigationRepository;

        public InvestigationService(InvestigationRepository investigationRepository)
        {
            this.investigationRepository = investigationRepository;
        }

        public async Task<InvestigationTaskBodyParts> GetInvestigationTasks(int eventId)
        {
            var investigationData = new InvestigationTaskBodyParts();            
            investigationData.InvestigationDataTask = await investigationRepository.GetInvestigationTask(eventId);
            investigationData.BodyParts = await investigationRepository.GetBodyPartEvent(eventId);

            return investigationData;
        }

        public async Task<IEnumerable<InvestigationBodyPartEntity>> GetBodyPart()
        {
            return await investigationRepository.GetBodyPart();
        }       

        public async Task<IEnumerable<InvestigationConditionsWeatherEntity>> GetConditionsWeather()
        {
            return await investigationRepository.GetConditionsWeather();
        }

        public async Task<IEnumerable<InvestigationConditionsRoadEntity>> GetConditionsRoad()
        {
            return await investigationRepository.GetConditionsRoad();
        }

        public string[] GetRegionBrazil()
        {
            return investigationRepository.GetRegionBrazil();
        }

        public async Task<IEnumerable<InvestigationVehicleTypeEntity>> GetVehicleType()
        {
            return await investigationRepository.GetVehicleType();
        }

        public async Task<InvestigationResponse> InvestigationEdition(CommandInvestigationTask investigationData)
        {
            var response = new InvestigationResponse();

            var investigationUpdate = await investigationRepository.InvestigationEdition(investigationData.Investigation);
            var taskUpdate = await investigationRepository.InvestigationTaskEdition(investigationData.Task);
            var bodyPartsEdited = await investigationRepository.IncludeBodyPartEvent(investigationData.BodyParts);

            response.InvestigationId = investigationData.Investigation.InvestigationId;
            response.TaskId = investigationData.Task.TaskId;

            if (investigationUpdate && taskUpdate && bodyPartsEdited)
            {
                response.Success = true;
                response.Message = "Investigation changed";
            }
            else
            {
                response.Success = false;
                response.Message = "Investigation not changed";
            }

            return response;
        }
    }
}
