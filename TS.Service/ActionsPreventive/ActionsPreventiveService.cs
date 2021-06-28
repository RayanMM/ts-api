using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Dtos.ActionsPreventive;
using TS.Domain.Entities.ActionsPreventive;
using TS.Infra.Repositories.ActionsPreventive;
using TS.Service.Base;

namespace TS.Service.ActionsPreventive
{
    public class ActionsPreventiveService : IService
    {
        private readonly ActionsPreventiveRepository actionsPreventiveRepository;

        public ActionsPreventiveService(ActionsPreventiveRepository actionsPreventiveRepository)
        {
            this.actionsPreventiveRepository = actionsPreventiveRepository;
        }
        public async Task<IEnumerable<ActionsPreventiveActionSubjectEntity>> GetActionsPreventiveActionSubject()
        {
            return await actionsPreventiveRepository.GetActionsPreventiveActionSubject();
        }
        public async Task<IEnumerable<ActionsPreventiveActionEntity>> GetActionsPreventiveAction(int eventId)
        {
            return await actionsPreventiveRepository.GetActionsPreventiveAction(eventId);
        }
        public async Task<ActionsPreventiveResponse> IncludeActionsPreventiveAction(List<ActionsPreventiveActionEntity> actionsPreventiveAction)
        {
            return await actionsPreventiveRepository.IncludeActionsPreventiveAction(actionsPreventiveAction);
        }
    }
}
