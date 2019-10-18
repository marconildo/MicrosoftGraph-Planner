using System.Threading.Tasks;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Service
{
    public class PlannerService : GraphService
    {
        public PlannerService()
        {
            BaseUri = "/planner/plans";
        }

        public async Task<IPlannerTasksCollectionPage> RecuperarPlanTasks(string idPlano)
        {
            var url = $"{BaseUri}/{idPlano}/tasks";
            var tasks = await GetResponse<PlannerTasksCollectionResponse>(url);
            return tasks.Value;
        }

        public async Task<IPlannerPlanBucketsCollectionPage> RecuperarPlanBuckets(string idPlano)
        {
            var url = $"{BaseUri}/{idPlano}/buckets";
            var buckets = await GetResponse<PlannerPlanBucketsCollectionResponse>(url);
            return buckets.Value;
        }

        public async Task<PlannerPlan> RecuperarPlanoPorId(string idPlano)
        {
            var url = $"{BaseUri}/{idPlano}";
            var plan = await GetResponse<PlannerPlan>(url);
            return plan;
        }

        public async Task<PlannerTaskDetails> RecuperarDetalheTarefaPorId(string idTarefa)
        {
            var url = $"{BaseUri.Replace("/plans", "/tasks")}/{idTarefa}/details";
            var plan = await GetResponse<PlannerTaskDetails>(url);
            return plan;
        }
    }
}
