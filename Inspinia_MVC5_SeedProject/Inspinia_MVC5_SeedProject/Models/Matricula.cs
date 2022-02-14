using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Matricula
    {

        [Key()]

        public int MatriculaId { get; set; }
        /* public int QuantidadeDias { get; set; }
         public int QuantidadeHoras { get; set; }
         public string primeiroDia { get; set; }
         public string segundoDia { get; set; }
         public string terceiroDia { get; set; }
         public DateTime? horaInicio { get; set; }
         public DateTime? horaFim { get; set; }
        */

        [DataType(DataType.Date)]
        public DateTime? DataMatricula { get; set; }



        [ForeignKey("Aluno")]
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }

        [NotMapped]
        public virtual List<MatriculaCursoView> listaViewMatriculaCurso { get; set; }


        [NotMapped]
        public virtual MatriculaCursoView objMatView { get; set; }

       [NotMapped]
        public virtual MatriculasAtivasView objMatriculaAtivaView { get; set; }






            [NotMapped]
        public virtual Pagamento objPagamento { get; set; }


        public Matricula()
        {
            listaViewMatriculaCurso = new List<MatriculaCursoView>();
            objPagamento = new Pagamento();
        }






    }





}