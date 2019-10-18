using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Data
{
    public class PlannerPlans
    {
        public PlannerPlans()
        {
            Tasks = new HashSet<PlannerTasks>();
            Buckets = new HashSet<PlannerBuckets>();
        }
        public string Id { get; set; }
        public string IdGroup { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Owner { get; set; }
        public string Title { get; set; }
        public virtual Groups Group { get; set; }
        public virtual ICollection<PlannerTasks> Tasks { get; set; }
        public virtual ICollection<PlannerBuckets> Buckets { get; set; }

        public static PlannerPlans FromModel(PlannerPlan model)
        {
            var vm = Mapper.Map<PlannerPlan, PlannerPlans>(model);
            return vm;
        }
    }
}
