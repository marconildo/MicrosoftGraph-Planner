using System.Threading.Tasks;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Service
{
    public class MeService : GraphService
    {
        public MeService()
        {
            BaseUri = "/me";
        }

        public async Task<IUserMemberOfCollectionWithReferencesPage> RecuperarMeusGrupos()
        {
            var url = $"{BaseUri}/memberOf";
            var grupos = await GetResponse<UserMemberOfCollectionWithReferencesResponse>(url);
            return grupos.Value;
        }
    }
}
