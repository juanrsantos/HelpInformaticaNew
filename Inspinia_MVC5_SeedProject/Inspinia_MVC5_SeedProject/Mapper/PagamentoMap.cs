using Inspinia_MVC5_SeedProject.Models;
using System.Data.Entity.ModelConfiguration;

namespace Inspinia_MVC5_SeedProject.Mapper
{
    public class PagamentoMap : EntityTypeConfiguration<Pagamento>
    {

        public PagamentoMap()
        {
            ToTable("Pagamento");
            HasKey(x => x.PagamentoId);

        }




    }
}