using EntityFrameworkEscola.DataAccess.Map;
using Inspinia_MVC5_SeedProject.Mapper;
using Inspinia_MVC5_SeedProject.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Inspinia_MVC5_SeedProject
{
    public class Contexto : DbContext
    {

        public DbSet<Aluno> alunos { get; set; }
        public DbSet<Curso> cursos { get; set; }
        public DbSet<Matricula> matriculas { get; set; }
        public DbSet<MatriculaCurso> matriculacurso { get; set; }
        public DbSet<PagamentoMensalidade> pagamentomensalidades { get; set; }

        public DbSet<Pagamento> pagamentos { get; set; }


        public Contexto() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //Aqui vamos remover a pluralização padrão do Etity Framework que é em inglês
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            /*modelBuilder.Entity<Matricula>()
                  .HasMany<Curso>(s => s.Cursos)
                  .WithMany(c => c.Matriculas)
                  .Map(cs =>
                  {
                      cs.MapLeftKey("MatriculaId");
                      cs.MapRightKey("CursoId");
                      cs.ToTable("MatriculaCurso");
                  });*/







            modelBuilder.Configurations.Add(new AlunoMap());
            modelBuilder.Configurations.Add(new MatriculaMap());
            modelBuilder.Configurations.Add(new CursoMap());


            modelBuilder.Configurations.Add(new PagamentoMap());






        }






    }
}