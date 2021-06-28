using System;

namespace TS.Domain.Entities.Investigation
{
    public class InvestigationTaskEntity
    {
        public int TaskId { get; set; }
        public int EventId { get; set; }
        public string TaskExecuted { get; set; }
        public int TaskIsStandardized { get; set; }
        public int TaskTimeFunctionId { get; set; }
        public int TaskAble { get; set; }
        public DateTime TaskLastTraining { get; set; }
        public int TaskSimilarSituations { get; set; }
        public string TaskWichSimilarSituations { get; set; }
        public string TaskAction { get; set; }
        public string TaskImprovement { get; set; }
        public int TaskIsRightTool { get; set; }
        public int TaskEvidences { get; set; }
    }
}
