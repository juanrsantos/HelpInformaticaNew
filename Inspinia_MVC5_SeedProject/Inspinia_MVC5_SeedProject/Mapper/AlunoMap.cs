using Inspinia_MVC5_SeedProject.Models;
using System.Data.Entity.ModelConfiguration;

namespace Inspinia_MVC5_SeedProject.Mapper
{
    public class AlunoMap : EntityTypeConfiguration<Aluno>
    {

        public AlunoMap()
        {
            ToTable("Aluno");

            Property(x => x.Nome).IsRequired();





        }
    }
}