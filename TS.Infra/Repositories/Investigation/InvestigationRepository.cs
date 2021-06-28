using Dapper;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TS.Domain.Dtos.Investigation;
using TS.Domain.Entities.Investigation;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.Investigation
{
    public class InvestigationRepository : IRepository
    {
        private readonly DbContext context;
        public InvestigationRepository(DbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<InvestigationBodyPartEntity>> GetBodyPart()
        {
            return await context.Connection.QueryAsync<InvestigationBodyPartEntity>("select BodyPartId, BodyPartName from BodyPart");
        }

        public async Task<List<InvestigationBodyPartEventEntity>> GetBodyPartEvent(int eventID)
        {

            var listBodyParts = new List<InvestigationBodyPartEventEntity>();

            var dataBodyParts = await context.Connection.QueryAsync<InvestigationBodyPartEventEntity>("select BodyPartId, EventId from BodyPartEvent WHERE EventId = @eventID", new { eventID });
            foreach (var dataBodyPart in dataBodyParts)
            {
                listBodyParts.Add(new InvestigationBodyPartEventEntity { BodyPartId = dataBodyPart.BodyPartId, EventId = dataBodyPart.EventId, IsActive = dataBodyPart.IsActive });
            }

            return listBodyParts;
        }

        public async Task<IEnumerable<InvestigationConditionsWeatherEntity>> GetConditionsWeather()
        {
            return await context.Connection.QueryAsync<InvestigationConditionsWeatherEntity>("SELECT ConditionsWeatherId, ConditionsWeatherName, IsEnabled FROM ConditionsWeather");
        }

        public async Task<IEnumerable<InvestigationConditionsRoadEntity>> GetConditionsRoad()
        {
            return await context.Connection.QueryAsync<InvestigationConditionsRoadEntity>("SELECT ConditionsRoadId, ConditionsRoadName, IsEnabled FROM ConditionsRoad");
        }


        public async Task<InvestigationData> GetInvestigationTask(int eventId)
        {

            var queryInvestigation = @"SELECT InvestigationId, t.EventId, InvestigationDriverName, InvestigationDriverLicense, InvestigationLicenseDueDate, InvestigationLicenseEmissionCountry, InvestigationLicenseEmissionState, InvestigationLastTrainingDefensiveDriving, InvestigationIsCompanyVehicle, VehicleTypeId, InvestigationVehicleYear, InvestigationDriverTrainned, InvestigationVehiclePlaces, InvestigationVehicleSafetyBelt, InvestigationVehicleLastInspection, InvestigationVehicleNPassengers, InvestigationDriverSafetyBelt, InvestigationOccupantsSafetyBelt, InvestigationOccupantsComments, InvestigationIsDriverAllowed, InvestigationVehicleMaintenance, InvestigationUseDefensiveDriving, InvestigationDriverDisturbed, InvestigationDriverDistracted, InvestigationDriverSleep, InvestigationSpeedLimit, InvestigationRealSpeed, InvestigationLoadWeight, ConditionsWeatherId, ConditionsRoadId, InvestigationUseVehicle,
                                       TaskId, TaskExecuted, TaskIsStandardized, TaskTimeFunctionId, TaskAble, TaskLastTraining, TaskSimilarSituations, TaskWichSimilarSituations, TaskAction, TaskImprovement, TaskIsRightTool, TaskEvidences
                                       FROM Investigation i join Task t on I.EventId = T.EventId  where i.EventId = @eventId";
            return await context.Connection.QueryFirstOrDefaultAsync<InvestigationData>(queryInvestigation, new { eventId });

        }

        public string[] GetRegionBrazil()
        {
            var siglasEstados = new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
            return siglasEstados;
        }

        public async Task<IEnumerable<InvestigationVehicleTypeEntity>> GetVehicleType()
        {
            return await context.Connection.QueryAsync<InvestigationVehicleTypeEntity>("SELECT VehicleTypeId, VehicleTypeName, IsEnabled FROM VehicleType");
        }

        public async Task<bool> InvestigationEdition(InvestigationEntity investigationData)
        {
            try
            {
                if (investigationData.InvestigationUseVehicle)
                    return true;

                int exists = await context.Connection.QueryFirstOrDefaultAsync<int>(@"select count(*) from Investigation where Investigationid = @InvestigationId", new { InvestigationId = investigationData.InvestigationId });

                if (exists == 1)
                {
                    var dateAux = new DateTime(0001, 01, 01, 00, 0, 0);

                    investigationData.InvestigationLicenseDueDate = investigationData.InvestigationLicenseDueDate == dateAux ? null : investigationData.InvestigationLicenseDueDate;
                    investigationData.InvestigationLastTrainingDefensiveDriving = investigationData.InvestigationLastTrainingDefensiveDriving == dateAux ? null : investigationData.InvestigationLastTrainingDefensiveDriving;
                    investigationData.InvestigationVehicleLastInspection = investigationData.InvestigationVehicleLastInspection == dateAux ? null : investigationData.InvestigationVehicleLastInspection;

                    string queryUpdate = "Update Investigation set InvestigationDriverName = @InvestigationDriverName, InvestigationDriverLicense = @InvestigationDriverLicense, InvestigationLicenseDueDate = @InvestigationLicenseDueDate, InvestigationLicenseEmissionCountry = @InvestigationLicenseEmissionCountry, InvestigationLicenseEmissionState = @InvestigationLicenseEmissionState, InvestigationLastTrainingDefensiveDriving = @InvestigationLastTrainingDefensiveDriving, InvestigationIsCompanyVehicle = @InvestigationIsCompanyVehicle, VehicleTypeId = @VehicleTypeId, InvestigationVehicleYear = @InvestigationVehicleYear, InvestigationDriverTrainned = @InvestigationDriverTrainned, InvestigationVehiclePlaces = @InvestigationVehiclePlaces, InvestigationVehicleSafetyBelt = @InvestigationVehicleSafetyBelt, InvestigationVehicleLastInspection = @InvestigationVehicleLastInspection, InvestigationVehicleNPassengers = @InvestigationVehicleNPassengers, InvestigationDriverSafetyBelt = @InvestigationDriverSafetyBelt, InvestigationOccupantsSafetyBelt = @InvestigationOccupantsSafetyBelt, InvestigationOccupantsComments = @InvestigationOccupantsComments, InvestigationIsDriverAllowed = @InvestigationIsDriverAllowed, InvestigationVehicleMaintenance = @InvestigationVehicleMaintenance, InvestigationUseDefensiveDriving = @InvestigationUseDefensiveDriving, InvestigationDriverDisturbed = @InvestigationDriverDisturbed, InvestigationDriverDistracted = @InvestigationDriverDistracted, InvestigationDriverSleep = @InvestigationDriverSleep, InvestigationSpeedLimit = @InvestigationSpeedLimit, InvestigationRealSpeed = @InvestigationRealSpeed, InvestigationLoadWeight = @InvestigationLoadWeight, ConditionsWeatherId = @ConditionsWeatherId, ConditionsRoadId = @ConditionsRoadId, InvestigationUseVehicle = 1 where  InvestigationId = @InvestigationId";
                    await context.Connection.ExecuteAsync(queryUpdate, investigationData);
                }
                else
                    throw new Exception("Investigation not found");
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return true;
        }

        public async Task<bool> InvestigationTaskEdition(InvestigationTaskEntity taskData)
        {
            int exists = await context.Connection.QueryFirstOrDefaultAsync<int>(@"select count(*) from task where taskId = @taskId", new { taskId = taskData.TaskId });
            try
            {
                if (exists == 1)
                {
                    string queryUpdate = "Update task set TaskExecuted = @TaskExecuted, TaskIsStandardized = @TaskIsStandardized, TaskTimeFunctionId = @TaskTimeFunctionId, TaskAble = @TaskAble, TaskLastTraining = @TaskLastTraining, TaskSimilarSituations = @TaskSimilarSituations, TaskWichSimilarSituations = @TaskWichSimilarSituations, TaskAction = @TaskAction, TaskImprovement = @TaskImprovement, TaskIsRightTool = @TaskIsRightTool, TaskEvidences = @TaskEvidences where TaskId = @TaskId ";
                    await context.Connection.ExecuteAsync(queryUpdate, taskData);
                }
                else
                    throw new Exception("task not found");
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IncludeBodyPartEvent(List<InvestigationBodyPartEventEntity> bodyPartsEvent)
        {
            bool status = true;
            try
            {
                if (bodyPartsEvent.Count > 0)
                {
                    string queryDelete = "DELETE FROM BodyPartEvent WHERE EventId = @EventId";
                    await context.Connection.ExecuteAsync(queryDelete, bodyPartsEvent);

                    string queryInsert = "INSERT INTO BodyPartEvent(EventId, BodyPartId, IsActive) VALUES (@EventId, @BodyPartId, @IsActive)";
                    await context.Connection.ExecuteAsync(queryInsert, bodyPartsEvent);
                }
                else
                    throw new Exception("Sem informação para cadastro");
            }
            catch
            {
                status = false;
            }
            return status;
        }
    }
}
