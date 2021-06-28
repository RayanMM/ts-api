using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Dtos.ActionsPreventive;
using TS.Domain.Entities.ActionsPreventive;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.ActionsPreventive
{
    public class ActionsPreventiveRepository : IRepository
    {
        private readonly DbContext context;
        public ActionsPreventiveRepository(DbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ActionsPreventiveActionSubjectEntity>> GetActionsPreventiveActionSubject()
        {
            return await context.Connection.QueryAsync<ActionsPreventiveActionSubjectEntity>("select * from ActionSubject");
        }
        public async Task<IEnumerable<ActionsPreventiveActionEntity>> GetActionsPreventiveAction(int eventId)
        {
            return await context.Connection.QueryAsync<ActionsPreventiveActionEntity>("select EventId,ActionSubjectId,ActionName, RowNumber from Action WHERE EventId = @eventId", new { eventId });
        }
        public async Task<ActionsPreventiveResponse> IncludeActionsPreventiveAction(List<ActionsPreventiveActionEntity> actionsPreventiveAction)
        {
            var response = new ActionsPreventiveResponse();
            response.Message = "Action registered";
            response.Success = true;
            try
            {
                if (actionsPreventiveAction.Count > 0)
                {
                    string queryDelete = "DELETE FROM Action WHERE EventId = @EventId";
                    await context.Connection.ExecuteAsync(queryDelete, actionsPreventiveAction);

                    string queryInsert = "INSERT INTO Action (EventId, ActionSubjectId, ActionName, RowNumber) VALUES (@EventId, @ActionSubjectId, @ActionName, @RowNumber)";
                    await context.Connection.ExecuteAsync(queryInsert, actionsPreventiveAction);
                }
                else
                    throw new Exception("Sem informação");
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
