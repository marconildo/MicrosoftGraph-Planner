using System;
using AutoMapper;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Data
{
    public class PlannerChecklistItems
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public bool? IsChecked { get; set; }
        public string Title { get; set; }
        public string OrderHint { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public virtual PlannerTasks Task { get; set; }

        public static PlannerChecklistItems FromModel(PlannerChecklistItem model)
        {
            var vm = Mapper.Map<PlannerChecklistItem, PlannerChecklistItems>(model);
            return vm;
        }
    }
}
