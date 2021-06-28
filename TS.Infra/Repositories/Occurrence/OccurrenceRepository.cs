using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Dtos.Occurrence;
using TS.Domain.Entities.Occurrence;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.Occurrence
{
    public class OccurrenceRepository : IRepository
    {
        private readonly DbContext context;
        public OccurrenceRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<OccurrenceClassificationEntity>> GetClassification()
        {
            return await context.Connection.QueryAsync<OccurrenceClassificationEntity>(@"SELECT OccurrenceClassificationId, OccurrenceClassificationName FROM  OccurrenceClassification");
        }

        public async Task<IEnumerable<OccurrenceTypeEntity>> GetOccurrenceType()
        {
            return await context.Connection.QueryAsync<OccurrenceTypeEntity>(@"SELECT OccurrenceTypeId, OccurrenceTypeName, IsEnabled FROM  OccurrenceType");
        }

        public async Task<IEnumerable<OccurrenceJobEntity>> GetOccurrenceJob()
        {
            return await context.Connection.QueryAsync<OccurrenceJobEntity>(@"select OccupationId, OccupationName, IsEnabled from Occupation");
        }

        public async Task<IEnumerable<OccurrenceFacilityEntity>> GetOccurrenceFacilities()
        {
            return await context.Connection.QueryAsync<OccurrenceFacilityEntity>(@"SELECT FacilityId, FacilityName, FacilityCity, FacilityState, FacilityAdress, FacilityAdressNumber, IsEnabled FROM Facility");
        }

        public async Task<IEnumerable<OccurrenceDepartamentEntity>> GetOccurrenceDepartament()
        {
            return await context.Connection.QueryAsync<OccurrenceDepartamentEntity>(@"SELECT DepartamentId, DepartamentGroupId, DepartamentName, IsEnabled FROM Departament");
        }

        public async Task<IEnumerable<OccurrenceContractTypeEntity>> GetOccurrenceContractType()
        {
            return await context.Connection.QueryAsync<OccurrenceContractTypeEntity>(@"SELECT ContractTypeId, ContractTypeName, IsEnabled FROM ContractType");
        }

        public async Task<IEnumerable<OccurrenceOutSourcedCompaniesEntity>> GetOccurrenceOutSourcedCompanies()
        {
            return await context.Connection.QueryAsync<OccurrenceOutSourcedCompaniesEntity>(@"SELECT OutSourcedCompaniesId, OutSourcedCompaniesCode, OutSourcedCompaniesName, OutSourcedCompaniesAdress, IsEnabled FROM OutSourcedCompanies");
        }

        public async Task<IEnumerable<OccurrenceHappenedEntity>> GetOccurrenceHappened(int happenedGroupId)
        {
            return await context.Connection.QueryAsync<OccurrenceHappenedEntity>(@"SELECT HappenedId, HappenedGroupId, HappenedName, IsEnabled FROM Happened WHERE HappenedGroupId = @happenedGroupId", new { happenedGroupId });
        }

        public async Task<IEnumerable<OccurrenceHappenedGroupEntity>> GetOccurrenceHappenedGroup()
        {
            return await context.Connection.QueryAsync<OccurrenceHappenedGroupEntity>(@"SELECT HappenedGroupId, HappenedGroupName, IsEnabled FROM HappenedGroup");
        }

        public async Task<OccurrenceResponse> OccurrenceEdition(OccurrenceEventEntity occurrenceData)
        {
            int exists = await context.Connection.QueryFirstOrDefaultAsync<int>(@"select count(*) from event where eventId = @eventId", new { eventId = occurrenceData.EventId });

            var response = new OccurrenceResponse();
            response.Success = true;

            try
            {
                var dateAux = new DateTime(0001, 01, 01, 00, 0, 0);

                occurrenceData.DateTimeStamp = occurrenceData.DateTimeStamp == dateAux ? null : occurrenceData.DateTimeStamp;
                occurrenceData.AbsenceDateTimeIni = occurrenceData.AbsenceDateTimeIni == dateAux ? null : occurrenceData.AbsenceDateTimeIni;
                occurrenceData.AbsenceDateTimeEnd = occurrenceData.AbsenceDateTimeEnd == dateAux ? null : occurrenceData.AbsenceDateTimeEnd;

                if (exists == 1)
                {                  
                    string queryUpdate = "UPDATE event set OccurrenceClassificationId=@OccurrenceClassificationId,OccurrenceTypeId=@OccurrenceTypeId,EventIdentification=@EventIdentification,EventInjuredPersonName=@EventInjuredPersonName,OccupationId=@OccupationId,FacilityId=@FacilityId,DepartamentId=@DepartamentId,EventSupervisorName=@EventSupervisorName,UserId=@UserId,ContractTypeId=@ContractTypeId,OutSourcedCompaniesId=@OutSourcedCompaniesId,HappenedGroupId=@HappenedGroupId,HappenedId=@HappenedId,EventDescription=@EventDescription,EventActions=@EventActions,EventSIFStatus=@EventSIFStatus,EventSIFCriticality=@EventSIFCriticality,DateTimeStamp=@DateTimeStamp,AbsenceDateTimeIni=@AbsenceDateTimeIni,AbsenceDateTimeEnd=@AbsenceDateTimeEnd,IsVehicleEvent=@IsVehicleEvent,IsClosed=@IsClosed,UserDepartamentId=@UserDepartamentId WHERE EventId=@EventId";
                    await context.Connection.ExecuteAsync(queryUpdate, occurrenceData);
                    response.Message = "Occurrence successfully changed";
                }
                else
                    throw new Exception("Occurrence not found");
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }

            response.OccurrenceId = occurrenceData.EventId;
            return response;
        }

        public async Task<OccurrenceResponse> OccurrenceInclude(OccurrenceEventEntity occurrenceData)
        {
            var response = new OccurrenceResponse();

            try
            {
                var dateAux = new DateTime(0001, 01, 01, 00, 0, 0);

                occurrenceData.DateTimeStamp = occurrenceData.DateTimeStamp == dateAux ? null : occurrenceData.DateTimeStamp;
                occurrenceData.AbsenceDateTimeIni = occurrenceData.AbsenceDateTimeIni == dateAux ? null : occurrenceData.AbsenceDateTimeIni;
                occurrenceData.AbsenceDateTimeEnd = occurrenceData.AbsenceDateTimeEnd == dateAux ? null : occurrenceData.AbsenceDateTimeEnd;

                string queryInsert = "INSERT INTO EVENT (OccurrenceClassificationId,OccurrenceTypeId,EventIdentification,EventInjuredPersonName,OccupationId,FacilityId,DepartamentId,EventSupervisorName,UserId,ContractTypeId,OutSourcedCompaniesId,HappenedGroupId,HappenedId,EventDescription,EventActions,EventSIFStatus,EventSIFCriticality,DateTimeStamp,AbsenceDateTimeIni,AbsenceDateTimeEnd,IsVehicleEvent,IsClosed,UserDepartamentId) OUTPUT INSERTED.EventID VALUES (@OccurrenceClassificationId,@OccurrenceTypeId,@EventIdentification,@EventInjuredPersonName,@OccupationId,@FacilityId,@DepartamentId,@EventSupervisorName,@UserId,@ContractTypeId,@OutSourcedCompaniesId,@HappenedGroupId,@HappenedId,@EventDescription,@EventActions,@EventSIFStatus,@EventSIFCriticality,@DateTimeStamp,@AbsenceDateTimeIni,@AbsenceDateTimeEnd,@IsVehicleEvent,@IsClosed,@UserDepartamentId)";
                var eventID = await context.Connection.QueryFirstOrDefaultAsync<int>(queryInsert, occurrenceData);

                if (eventID > 0)
                {
                    string queryInsertInvestigation = "Insert into Investigation (EventId) values (@investigationEventID)";
                    string queryInsertTask = "insert into Task(EventId) values (@taskEventID)";

                    await context.Connection.ExecuteAsync(queryInsertInvestigation, new { investigationEventID = eventID });
                    await context.Connection.ExecuteAsync(queryInsertTask, new { taskEventID = eventID });
                }

                response.OccurrenceId = eventID;
                response.Message = "Occurrencia successfully registered";
                response.Success = true;
            }
            catch (Exception e)
            {
                response.OccurrenceId = 0;
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<OccurrenceEventEntity> GetOccurrenceEventById(int eventId)
        {
            return await context.Connection.QueryFirstOrDefaultAsync<OccurrenceEventEntity>(@"select * from Event where eventId = @eventid", new { eventId });
        }

        public async Task<IEnumerable<OccurrenceEventEntity>> GetOccurrenceEvent()
        {
            return await context.Connection.QueryAsync<OccurrenceEventEntity>(@"select * from Event");
        }
    }
}
