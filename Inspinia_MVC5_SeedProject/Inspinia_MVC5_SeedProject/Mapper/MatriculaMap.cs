using Inspinia_MVC5_SeedProject.Models;
using System.Data.Entity.ModelConfiguration;

namespace Inspinia_MVC5_SeedProject.Mapper
{
    public class MatriculaMap : EntityTypeConfiguration<Matricula>
    {

        public MatriculaMap()
        {
            ToTable("Matricula");
            HasKey(x => x.MatriculaId);










        }
    }
}