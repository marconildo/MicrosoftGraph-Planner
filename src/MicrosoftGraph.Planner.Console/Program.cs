#undef SERVICO
//#define SERVICO


using AutoMapper;
using MicrosoftGraph.Planner.Console.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraph.Planner.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Mapper.Initialize(config =>
            //{
            //    config.CreateMap<Group, Groups>();
            //    config.CreateMap<PlannerPlan, PlannerPlans>();
            //    config.CreateMap<PlannerBucket, PlannerBuckets>();
            //    config.CreateMap<PlannerTask, PlannerTasks>();
            //    config.CreateMap<PlannerChecklistItem, PlannerChecklistItems>();
            //    config.CreateMap<User, Users>();
            //});

#if (SERVICO)
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new PlannerBIService() };
            ServiceBase.Run(ServicesToRun);
#else
            var importacao = new Importacao();
            importacao.Executar().Wait();
#endif
        }
    }
}
