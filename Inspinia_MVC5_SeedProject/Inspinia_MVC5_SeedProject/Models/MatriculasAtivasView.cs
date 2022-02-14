using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class MatriculasAtivasView
    {

        [NotMapped]
        public int? MatriculaId { get; set; }

        [NotMapped]
        public DateTime? DataMatricula { get; set; }


        [NotMapped]
        public string NomeAluno { get; set; }


        [NotMapped]
        public IList<Matricula> listaMatriculasAtivasView;


        public MatriculasAtivasView()
        {
            listaMatriculasAtivasView = new List<Matricula>();
        }



    }
}