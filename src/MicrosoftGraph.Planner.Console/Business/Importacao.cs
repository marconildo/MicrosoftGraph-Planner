using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using MicrosoftGraph.Planner.Console.Data;
using MicrosoftGraph.Planner.Console.Extensions;
using MicrosoftGraph.Planner.Console.Service;
using PlannerChecklistItems = MicrosoftGraph.Planner.Console.Data.PlannerChecklistItems;

namespace MicrosoftGraph.Planner.Console.Business
{
    public class Importacao
    {
        private readonly GroupsService _groupsService;
        private readonly PlannerService _plannetService;
        private readonly UsersService _usersService;
        private readonly ImportacaoContext _context;

        public Importacao()
        {
            _groupsService = new GroupsService();
            _plannetService = new PlannerService();
            _context = new ImportacaoContext();
            _usersService = new UsersService();
        }

        public async Task Executar()
        {
            //limpando as tabelas
            _context.Users.Clear();
            _context.PlannerChecklistsItems.Clear();
            _context.PlannerTasks.Clear();
            _context.PlannerBuckets.Clear();
            _context.PlannerPlans.Clear();
            _context.Groups.Clear();
            _context.SaveChanges();

            //Recuperar os grupos do usuario
            var meService = new MeService();
            var meusGrupos = await meService.RecuperarMeusGrupos();

            if (meusGrupos.Count > 0)
            {
                foreach (var o in meusGrupos)
                {
                    var grupo = (Group)o;
                    await TratarGrupo(grupo);
                }
            }
        }

        private async Task TratarGrupo(Group grupo)
        {
            //verifica se o grupo possui planos
            var planos = await _groupsService.RecuperarPlannerDoGrupo(grupo.Id);

            if (planos.Count > 0)
            {
                _context.Groups.Add(Groups.FromModel(grupo));
                _context.SaveChanges();

                foreach (var plano in planos)
                {
                    var planner = PlannerPlans.FromModel(plano);
                    planner.IdGroup = grupo.Id;
                    _context.PlannerPlans.Add(planner);
                    _context.SaveChanges();

                    await TratarPlanBuckets(plano.Id);
                    await TratarPlanTasks(plano.Id);
                }
            }
        }

        private async Task TratarPlanTasks(string idPlano)
        {
            var tasks = await _plannetService.RecuperarPlanTasks(idPlano);
            if (tasks.Count > 0)
            {
                foreach (var task in tasks.ToList())
                {
                    var objModel = PlannerTasks.FromModel(task);
                    objModel.PlanId = idPlano;

                    var bucket = _context.PlannerBuckets.FirstOrDefault(p => p.Id == objModel.BucketId);
                    if (bucket != null)
                        objModel.BucketName = bucket.Name;

                    var detalhes = _plannetService.RecuperarDetalheTarefaPorId(task.Id).Result;

                    if (detalhes != null)
                    {
                        if (task.HasDescription.HasValue && task.HasDescription.Value)
                            objModel.Description = detalhes.Description;
                    }

                    _context.PlannerTasks.Add(objModel);
                    _context.SaveChanges();

                    TratarAtribuicoes(task, objModel);

                    if (detalhes == null || !detalhes.Checklist.ToList().Any())
                        continue;

                    foreach (var checklist in detalhes.Checklist.ToList())
                    {
                        var checkModel = PlannerChecklistItems.FromModel(checklist.Value);
                        checkModel.Id = checklist.Key;
                        checkModel.TaskId = task.Id;

                        if (_context.PlannerChecklistsItems.FirstOrDefault(p => p.Id == checkModel.Id) == null)
                        {
                            _context.PlannerChecklistsItems.Add(checkModel);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        private void TratarAtribuicoes(PlannerTask task, PlannerTasks objModel)
        {
            if (task.Assignments.Count <= 0)
                return;

            foreach (var assignment in task.Assignments)
            {
                var usuario = _context.Users.FirstOrDefault(p => p.Id == assignment.Key);

                if (usuario == null)
                {
                    var user = _usersService.ObterUsuarioPorId(assignment.Key).Result;
                    usuario = Users.FromModel(user);
                    if (usuario.Tasks == null)
                        usuario.Tasks = new List<PlannerTasks>();
                    usuario.Tasks.Add(objModel);
                    _context.Users.Add(usuario);
                    _context.SaveChanges();
                }
                else
                {
                    usuario.Tasks.Add(objModel);
                    _context.Users.Attach(usuario);
                    _context.Entry(usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
        }

        private async Task TratarPlanBuckets(string idPlano)
        {
            var buckets = await _plannetService.RecuperarPlanBuckets(idPlano);

            if (buckets.Count > 0)
            {
                foreach (var bucket in buckets.ToList())
                {
                    var objModel = PlannerBuckets.FromModel(bucket);
                    objModel.PlanId = idPlano;
                    _context.PlannerBuckets.Add(objModel);
                    _context.SaveChanges();
                }
            }
        }
    }
}
