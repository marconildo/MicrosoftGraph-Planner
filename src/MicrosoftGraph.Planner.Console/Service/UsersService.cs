using System.Threading.Tasks;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Service
{
    public class UsersService : GraphService
    {
        public UsersService()
        {
            BaseUri = "/users";
        }

        public async Task<User> ObterUsuarioPorId(string idUsuario)
        {
            var url = $"{BaseUri}('{idUsuario}')";
            var user = await GetResponse<User>(url);
            return user;
        }
    }
}
