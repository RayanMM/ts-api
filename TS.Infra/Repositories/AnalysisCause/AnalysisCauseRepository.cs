using Dapper;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Dtos;
using TS.Domain.Dtos.AnalysisCause;
using TS.Domain.Entities.AnalysisCause;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.AnalysisCause
{
    public class AnalysisCauseRepository : IRepository
    {
        private readonly DbContext context;
        public AnalysisCauseRepository(DbContext context)
        {
            this.context = context;
        }
        public async Task<AnalysisOfCauseDto> GetAnalysisCausesWhy(int eventId)
        {
            var analysisOfCause = new AnalysisOfCauseDto();

            analysisOfCause.EventId = eventId;
            
            analysisOfCause.ListWhy = await context.Connection.QueryAsync<AnalysisCauseWhyEntity>("select EventId, WhySubjectId, WhyAnswer, RowNumber from Why WHERE EventId = @eventId", new { eventId });
            
            analysisOfCause.LessonLearned = await context.Connection.QueryFirstOrDefaultAsync<string>("SELECT LessonLearnedDescription from LessonLearned WHERE EventId = @eventId", new { eventId });

            return analysisOfCause;
        }

        public async Task<IEnumerable<AnalysisCauseWhySubjectEntity>> GetAnalysisCausesWhySubject()
        {
            return await context.Connection.QueryAsync<AnalysisCauseWhySubjectEntity>("select * from WhySubject");
        }

        public async Task<AnalysisCauseResponse> IncludeAnalysisCausesWhy(AnalisysOfCauseRequestDto command)
        {
            var response = new AnalysisCauseResponse();
            try
            {
                if (command.ListWhy.Count > 0)
                {
                    string queryDelete = "DELETE FROM Why WHERE EventId = @EventId";
                    await context.Connection.ExecuteAsync(queryDelete, new { eventId = command.EventId } );

                    foreach(var why in command.ListWhy)
					{
                        await context.Connection.ExecuteAsync("insert into Why(EventId,WhySubjectId, WhyAnswer, RowNumber) Values (@eventId, @whySubjectId, @whyAnswer, @rowNumber)", new { eventId = command.EventId, whySubjectId = why.WhySubjectId, whyAnswer = why.WhyAnswer, rowNumber = why.RowNumber });
                    }

                    //Checking if lesson learned exists
                    var lessonLearnedId = await context.Connection.QueryFirstOrDefaultAsync<int>("SELECT LessonLearnedId from LessonLearned WHERE EventId = @eventId", new { eventId = command.EventId });

                    if (lessonLearnedId > 0)
                        await context.Connection.ExecuteAsync("update LessonLearned Set LessonLearnedDescription = @lessonLearned WHERE LessonLearnedId = @lessonLearnedId", new { lessonLearned = command.LessonLearned, lessonLearnedId });
                    else 
                        await context.Connection.ExecuteAsync("insert into LessonLearned (eventId, LessonLearnedDescription) values (@eventId, @lessonLearned)", new { lessonLearned = command.LessonLearned, eventId = command.EventId });

                    response.Success = true;
                    response.Message = "Whys registered";
                }
                else
                    throw new Exception("Sem informação para registro");
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
