using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Aluno
    {

        [Key()]
        [Display(Name = "Codigo Aluno")]
        public int AlunoId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Data Nascimento")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public String DataNascimento { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DataExclusao { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DataAlteracao { get; set; }

        public int status { get; set; }


        [Display(Name = "Cpf")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cpf { get; set; }

        public string Foto { get; set; }

        [Display(Name = "Cep")]
        [Required(ErrorMessage = "Campo obrigatorio")]
        public string Cep { get; set; }

        [Display(Name = "Tipo Logradouro")]
        public string tipoLogradouro { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }



        public virtual ICollection<Matricula> Matricula { get; set; }

        [NotMapped]
        public IList<Curso> listaCursos { get; set; }

        [NotMapped]
        public List<MatriculasAtivasView> listaMatriculasAtivas { get; set; }





    }
}