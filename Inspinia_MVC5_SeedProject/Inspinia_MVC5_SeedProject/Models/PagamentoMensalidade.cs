using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class PagamentoMensalidade
    {
        [Key]
        public int IdPagamentoMensalidade { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime? DataVencimento { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataPagamento { get; set; }

        public Double ValorPagamento { get; set; }

        public Boolean? Pago { get; set; }

        [ForeignKey("Pagamento")]
        public int PagamentoId { get; set; }
        public virtual Pagamento Pagamento { get; set; }





    }
}