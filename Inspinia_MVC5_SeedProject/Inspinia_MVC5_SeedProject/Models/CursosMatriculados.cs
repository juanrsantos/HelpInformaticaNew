using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class CursosMatriculados
    {

        [NotMapped]
        public int? MatriculaCursoId { get; set; }

        [NotMapped]
        public string NomeCurso { get; set; }
        [NotMapped]
        public int Periodo { get; set; }
        [NotMapped]
        public DateTime? DataInicio { get; set; }
        [NotMapped]
        public DateTime? DataFim { get; set; }


    }
}