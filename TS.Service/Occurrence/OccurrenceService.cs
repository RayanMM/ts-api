using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Dtos.Occurrence;
using TS.Domain.Entities.Occurrence;
using TS.Infra.Repositories.Occurrence;
using TS.Service.Base;

namespace TS.Service.Occurrence
{
	public class OccurrenceService : IService
	{
		private readonly OccurrenceRepository occurrenceRepository;

		public OccurrenceService(OccurrenceRepository occurrenceRepository)
		{
			this.occurrenceRepository = occurrenceRepository;
		}

		public async Task<IEnumerable<OccurrenceClassificationEntity>> GetClassification()
		{
			return await occurrenceRepository.GetClassification();
		}

		public async Task<IEnumerable<OccurrenceTypeEntity>> GetOccurrenceType()
        {
			return await occurrenceRepository.GetOccurrenceType();
        }

		public async Task<IEnumerable<OccurrenceJobEntity>> GetOccurrenceJob()
		{
			return await occurrenceRepository.GetOccurrenceJob();
		}

		public	async Task<IEnumerable<OccurrenceFacilityEntity>> GetOccurrenceFacilities()
        {
			return await occurrenceRepository.GetOccurrenceFacilities();
        }

		public async Task<IEnumerable<OccurrenceDepartamentEntity>> GetOccurrenceDepartament()
		{
			return await occurrenceRepository.GetOccurrenceDepartament();
		}

		public async Task<IEnumerable<OccurrenceContractTypeEntity>> GetOccurrenceContractType()
		{
			return await occurrenceRepository.GetOccurrenceContractType();
		}

		public async Task<IEnumerable<OccurrenceOutSourcedCompaniesEntity>> GetOccurrenceOutSourcedCompanies()
		{
			return await occurrenceRepository.GetOccurrenceOutSourcedCompanies();
		}

		public async Task<IEnumerable<OccurrenceHappenedEntity>> GetOccurrenceHappened(int happenedGroupId)
		{
			return await occurrenceRepository.GetOccurrenceHappened(happenedGroupId);
		}

		public async Task<IEnumerable<OccurrenceHappenedGroupEntity>> GetOccurrenceHappenedGroup()
		{
			return await occurrenceRepository.GetOccurrenceHappenedGroup();
		}

		public async Task<OccurrenceResponse> OccurrenceEdition(OccurrenceEventEntity OccurrenceData)
		{
			return await occurrenceRepository.OccurrenceEdition(OccurrenceData);
		}

		public async Task<OccurrenceResponse> OccurrenceInclude(OccurrenceEventEntity OccurrenceData)
		{
			return await occurrenceRepository.OccurrenceInclude(OccurrenceData);
		}

		public async Task<OccurrenceEventEntity> GetOccurrenceEventById(int eventId)
		{
			return await occurrenceRepository.GetOccurrenceEventById(eventId);
		}

		public async Task<IEnumerable<OccurrenceEventEntity>> GetOccurrenceEvent()
		{
			return await occurrenceRepository.GetOccurrenceEvent();
		}


	}
}
