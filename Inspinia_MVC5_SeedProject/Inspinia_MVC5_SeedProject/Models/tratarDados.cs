namespace Inspinia_MVC5_SeedProject.Models
{
    public class tratarDados
    {


        public Aluno alunoTratar = new Aluno();
        public Aluno alterado = new Aluno();

        public Aluno tratarDadosAluno(Aluno aluno)
        {



            alterado.AlunoId = aluno.AlunoId;
            alterado.Nome = aluno.Nome;
            alterado.DataNascimento = aluno.DataNascimento.Replace('/', ' ').Replace(" ", "");
            alterado.DataExclusao = aluno.DataExclusao;
            alterado.DataAlteracao = aluno.DataAlteracao;
            alterado.status = aluno.status;
            alterado.Cpf = aluno.Cpf.Replace('.', ' ').Replace('-', ' ').Replace(" ", "");
            alterado.Foto = aluno.Foto;
            alterado.Cep = aluno.Cep.Replace('.', ' ').Replace('-', ' ').Replace(" ", "");
            alterado.tipoLogradouro = aluno.tipoLogradouro;
            alterado.Logradouro = aluno.Logradouro;
            alterado.Cidade = aluno.Cidade;
            alterado.Estado = aluno.Estado;

            if (aluno.Telefone != null)
            {
                alterado.Telefone = aluno.Telefone.Replace('.', ' ').Replace('(', ' ').Replace(')', ' ').Replace(" ", "");
            }

            if (aluno.Celular != null)
            {
                alterado.Celular = aluno.Celular.Replace('.', ' ').Replace('(', ' ').Replace(')', ' ').Replace(" ", "");
            }

            alterado.Email = aluno.Email;
            ///   alterado.listaCursos = aluno.listaCursos;
            //  alterado.listaCursosMatriculados = aluno.listaCursosMatriculados;



            return alterado;


        }



    }




}
