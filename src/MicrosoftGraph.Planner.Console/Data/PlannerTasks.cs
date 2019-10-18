using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Data
{
    public class PlannerTasks
    {
        public PlannerTasks()
        {
            Users = new HashSet<Users>();
            ChecklistsItems = new HashSet<PlannerChecklistItems>();
        }
        public string Id { get; set; }
        public string PlanId { get; set; }
        public string BucketId { get; set; }
        public string BucketName { get; set; }
        public string Title { get; set; }
        public string OrderHint { get; set; }
        public string Description { get; set; }
        public int? PercentComplete { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? DueDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public int? ChecklistItemCount { get; set; }
        public bool? HasDescription { get; set; }
        public int? ReferenceCount { get; set; }
        public string ConversationThreadId { get; set; }
        public virtual PlannerPlans PlannerPlans { get; set; }
        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<PlannerChecklistItems> ChecklistsItems { get; set; }

        public static PlannerTasks FromModel(PlannerTask model)
        {
            var vm = Mapper.Map<PlannerTask, PlannerTasks>(model);
            return vm;
        }
    }
}
