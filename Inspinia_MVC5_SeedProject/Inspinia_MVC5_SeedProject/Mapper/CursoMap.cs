
using Inspinia_MVC5_SeedProject.Models;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkEscola.DataAccess.Map
{
    public class CursoMap : EntityTypeConfiguration<Curso>
    {
        public CursoMap()
        {
            /*O método ToTable define qual o nome que será
            dado a tabela no banco de dados*/
            ToTable("Curso");

            //Definimos a propriedade CursoId como chave primária
            HasKey(x => x.CursoId);



        }
    }
}