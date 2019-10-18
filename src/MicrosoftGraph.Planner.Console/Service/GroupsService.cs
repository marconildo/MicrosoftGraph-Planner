using System.Threading.Tasks;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Service
{
    public class GroupsService : GraphService
    {
        public GroupsService()
        {
            BaseUri = "/groups";
        }

        public async Task<IPlannerPlansCollectionPage> RecuperarPlannerDoGrupo(string idGrupo)
        {
            var url = $"{BaseUri}/{idGrupo}/planner/plans";
            var planos = await GetResponse<PlannerPlansCollectionResponse>(url);
            return planos.Value;
        }
    }
}
