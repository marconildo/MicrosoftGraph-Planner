using System.Collections.Generic;
using AutoMapper;
using Microsoft.Graph;

namespace MicrosoftGraph.Planner.Console.Data
{
    public class Users
    {
        public Users()
        {
            Tasks = new HashSet<PlannerTasks>();
        }

        public string Id { get; set; }
        public string Department { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string JobTitle { get; set; }
        public string Mail { get; set; }
        public string MobilePhone { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }

        public virtual ICollection<PlannerTasks> Tasks { get; set; }

        public static Users FromModel(User model)
        {
            var vm = Mapper.Map<User, Users>(model);
            return vm;
        }
    }
}
