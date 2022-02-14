using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Curso
    {


        public int CursoId { get; set; }

        [Display(Name = "Curso")]
        public string NomeCurso { get; set; }

        [Display(Name = "Periodo(Meses)")]
        public int Periodo { get; set; }


        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Carga Horaria")]
        public int CargaHoraria { get; set; }

        [Display(Name = "Valor")]
        public float Valor { get; set; }


        public string Foto { get; set; }



    }
}