using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class MatriculaCurso
    {

        [Key]
        public int MatriculaCursoId { get; set; }

        [ForeignKey("Matricula")]
        public int MatriculaId { get; set; }
        public virtual Matricula Matricula { get; set; }

        [ForeignKey("Curso")]
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }

    }
}