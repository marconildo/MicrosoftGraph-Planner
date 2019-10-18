using System.Data.Entity;
using MySql.Data.Entity;

namespace MicrosoftGraph.Planner.Console.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ImportacaoContext : DbContext
    {
        public ImportacaoContext() : base("ImportacaoContextConn")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groups>()
                .HasMany(e => e.Plans)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.IdGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlannerPlans>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.PlannerPlans)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlannerPlans>()
                .HasMany(e => e.Buckets)
                .WithRequired(e => e.PlannerPlans)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlannerTasks>()
                .HasMany(e => e.ChecklistsItems)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlannerTasks>()
                .HasMany(s => s.Users)
                .WithMany(c => c.Tasks)
                .Map(cs =>
                {
                    cs.MapLeftKey("PlannerTasksId");
                    cs.MapRightKey("UsersId");
                    cs.ToTable("PlannerAssignments");
                });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<PlannerPlans> PlannerPlans { get; set; }
        public virtual DbSet<PlannerBuckets> PlannerBuckets { get; set; }
        public virtual DbSet<PlannerTasks> PlannerTasks { get; set; }
        public virtual DbSet<PlannerChecklistItems> PlannerChecklistsItems { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}