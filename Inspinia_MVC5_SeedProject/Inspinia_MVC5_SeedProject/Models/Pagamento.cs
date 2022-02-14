using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Pagamento
    {

        [Key()]
        public int PagamentoId { get; set; }
        public string DataCriacaoPagamento { get; set; }

        public Double ValorAberto { get; set; }

        public virtual ICollection<PagamentoMensalidade> PagamentoMensalidade { get; set; }

        [NotMapped]
        public double ValorPagamento { get; set; }

        [NotMapped]
        public DateTime? DataVencimento { get; set; }



        [ForeignKey("Matricula")]
        public int MatriculaId { get; set; }
        public virtual Matricula Matricula { get; set; }

      









    }
}