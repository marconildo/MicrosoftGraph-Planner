using AutoMapper;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Data
{
    public class PlannerBuckets
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PlanId { get; set; }
        public string OrderHint { get; set; }
        public virtual PlannerPlans PlannerPlans { get; set; }

        public static PlannerBuckets FromModel(PlannerBucket model)
        {
            var vm = Mapper.Map<PlannerBucket, PlannerBuckets>(model);
            return vm;
        }
    }
}
