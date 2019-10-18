using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Data
{
    public class Groups
    {
        public Groups()
        {
            Plans = new HashSet<PlannerPlans>();
        }

        public string Id { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public virtual ICollection<PlannerPlans> Plans { get; set; }

        public static Groups FromModel(Group model)
        {
            var vm = Mapper.Map<Group, Groups>(model);
            return vm;
        }
    }
}
