using System;
using System.Collections.Generic;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class MatriculaCursoView
    {

        public virtual int MatriculaId { get; set; }
        public virtual int AlunoId { get; set; }
        public virtual string NomeAluno { get; set; }

        public virtual DateTime? DataMatricula { get; set; }

        public List<Curso> listaCursosOfertados { get; set; }
        public List<Curso> listaCursosMatriculados { get; set; }

        public IList<Matricula> listaMatricula { get; set; }

        public MatriculaCursoView()
        {

            listaCursosOfertados = new List<Curso>();
            listaCursosMatriculados = new List<Curso>();
            listaMatricula = new List<Matricula>();
        }




    }
}