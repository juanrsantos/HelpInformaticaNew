using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class PagamentoMatriculaView
    {

        public virtual int MatriculaId { get; set; }
        public virtual DateTime? dataMatricula { get; set; }

        public virtual string NomeAluno { get; set; }
        public virtual string CaminhoFoto { get; set; }
      
        public virtual double CargaHoraria { get; set; }

        public virtual double Periodo { get; set; }
        public virtual double ValorPacote { get; set; }
        public virtual double ValorMensalidadeIguais { get; set; }



        //Pagamento Mensalidade
        public virtual int PagamentoMensalidadeId { get; set; }
        public virtual double ValorPagamento { get; set; }

        public virtual int PagamentoId { get; set; }

        public virtual DateTime? DataVencimento { get; set; }


   
        public virtual List<Curso> listaCursosMatriculados { get; set; }
        public virtual List<PagamentoMensalidade> listaPagamentoMensalidade { get; set; }



        public PagamentoMatriculaView()
        {
            listaCursosMatriculados = new List<Curso>();
            listaPagamentoMensalidade = new List<PagamentoMensalidade>();
        }


    }
}