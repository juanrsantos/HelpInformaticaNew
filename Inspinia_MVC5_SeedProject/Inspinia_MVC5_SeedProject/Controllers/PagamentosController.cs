using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class PagamentosController : Controller
    {
        private Contexto db = new Contexto();

        #region DECLARAÇÕES DE VARIAVEIS

   
        PagamentoMatriculaView ObjPagamentoView = new PagamentoMatriculaView();


        public IList<Matricula> listaMatriculas = new List<Matricula>();

        public IList<Curso> listaCursos = new List<Curso>();
        public IList<PagamentoMatriculaView> listaPagamentoMatriculaView = new List<PagamentoMatriculaView>();
        public IList<Pagamento> listaPagamentos = new List<Pagamento>();
        public IList<PagamentoMensalidade> listaPagamentoMensalidades = new List<PagamentoMensalidade>();



        #endregion
        // GET: Pagamentos
        public async Task<ActionResult> Index(string nome)
         {

            List<Matricula> listaMatriculasAtivas = new List<Matricula>();
            List<MatriculaCursoView> listMatriculaView = new List<MatriculaCursoView>();
            List<MatriculaCurso> listMatriculaCurso = new List<MatriculaCurso>(); 
          

            if(nome == null || nome =="")
            {
                listaMatriculasAtivas = db.matriculas.ToList();

                foreach (var item in listaMatriculasAtivas)
                {
                    Aluno objAluno = new Aluno();

                    MatriculaCursoView objMatriculaCursoView = new MatriculaCursoView();
                    objMatriculaCursoView.MatriculaId = item.MatriculaId;
                    objAluno = db.alunos.Where(x => x.AlunoId == item.AlunoId).FirstOrDefault();
                    objMatriculaCursoView.NomeAluno = objAluno.Nome;
                    objMatriculaCursoView.DataMatricula = item.DataMatricula;
                    listMatriculaView.Add(objMatriculaCursoView);
                  
                }
            }
            else
            {
                Matricula objMatricula = new Matricula();

                listaMatriculasAtivas = db.matriculas.ToList();

                foreach(var item in listaMatriculasAtivas)
                {
                    if(item.Aluno.Nome.Contains(nome))
                    {
                        MatriculaCursoView objMatriculaCursoView = new MatriculaCursoView();
                        objMatriculaCursoView.MatriculaId = item.MatriculaId;
                        objMatriculaCursoView.NomeAluno = item.Aluno.Nome;
                        objMatriculaCursoView.DataMatricula = item.DataMatricula;
                        listMatriculaView.Add(objMatriculaCursoView);

                    }
                }
            }
              return View(listMatriculaView);
        }


        public async Task<ActionResult> gerarMatricula(int? idAluno)
        {

            Matricula objMatricula = new Matricula();
            Aluno pesquisarAluno = db.alunos.Find(idAluno);
            Matricula novaMatricula = new Matricula();


            /* using (var consulta = new Contexto())
             {
                 novaMatricula = consulta.matriculas.Where(x => x.AlunoId == idAluno).FirstOrDefault();

             }*/ 

            objMatricula.Aluno = pesquisarAluno;
            objMatricula.AlunoId = (int)idAluno;
            objMatricula.DataMatricula = DateTime.Now;

            db.matriculas.Add(objMatricula);
            db.SaveChanges();


            return RedirectToAction("Details", new { id = idAluno });




        }


        // GERAR PAGAMENTO 
        [HttpPost]
        public JsonResult GerarPagamento(Pagamento dados)

        {
            Pagamento objPagamento = new Pagamento();
            PagamentoMatriculaView objPagamentoMatriculaView = new PagamentoMatriculaView();
            var result = false;
            try
            {
                if(dados != null)
                {
                    result = VerificarPagamentoJaExistente(dados);
                   
                    if (!result)
                    {
                        objPagamento.DataCriacaoPagamento = DateTime.Now.ToString("dd/MM/yyyy");
                        objPagamento.ValorAberto = dados.ValorAberto;
                        objPagamento.MatriculaId = (int)dados.MatriculaId;
                        objPagamento.Matricula = db.matriculas.Where(x => x.MatriculaId == objPagamento.MatriculaId).FirstOrDefault();
                        db.pagamentos.Add(objPagamento);
                        db.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
            
                }
            }
            catch(Exception ex)
            {
                result = false;
                TempData["warning"] = ex.Message;
            }
            return Json(new { success = result });
        }


        // GERAR PAGAMENTO MENSALIDADE (PARCELA)
        [HttpPost]
        public JsonResult gerarPagamentoMensalidade(PagamentoMatriculaView dados)
        {
            Pagamento obPagamento = new Pagamento();
            PagamentoMensalidade objPagamentoMensalidade = new PagamentoMensalidade();
            obPagamento = db.pagamentos.Where(x => x.MatriculaId == dados.MatriculaId).FirstOrDefault();
            objPagamentoMensalidade.DataVencimento = dados.DataVencimento;
            objPagamentoMensalidade.Pagamento = obPagamento;
            objPagamentoMensalidade.ValorPagamento = dados.ValorPagamento;


            

            if (dados.ValorPagamento != 0)
            {
                if (obPagamento != null)
                {
                    if (dados.ValorPagamento <= obPagamento.ValorAberto)
                    {
                        List<PagamentoMensalidade> listaPagMensalidade = new List<PagamentoMensalidade>();
                        listaPagMensalidade = db.pagamentomensalidades.Where(x => x.PagamentoId == obPagamento.PagamentoId).ToList();


                        if (listaPagMensalidade.Count != 0)
                        {
                            var existeDataVencimento = false;
                            double somaMensalidade = 0.0;
                            foreach (var item in listaPagMensalidade)
                            {

                                somaMensalidade = somaMensalidade + item.ValorPagamento;
                                if (item.DataVencimento == dados.DataVencimento)
                                {
                                    existeDataVencimento = true;
                                }
                            }

                            somaMensalidade = somaMensalidade + dados.ValorPagamento;

                            if (somaMensalidade <= obPagamento.ValorAberto)
                            {
                                if (existeDataVencimento != true)
                                {

                                    db.pagamentomensalidades.Add(objPagamentoMensalidade);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        erro = "Não é possivel fazer o mesmo lançamento no mesmo mês"
                                    });

                                }
                            }
                            else
                            {
                                return Json(new
                                {
                                    success = false,
                                    erro = "Lançamento maior do que valor aberto"
                                });
                            }
                        }
                        else
                        {
                            db.pagamentomensalidades.Add(objPagamentoMensalidade);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            erro = "Lançamento maior do que valor aberto"
                        });

                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        erro = "Gerar Pagamento Antes"
                    });
                }
            }
           

            return Json (new {success= true });
        }

        // GET: /Pagamentos/Contrato/5
        public ActionResult Contrato(int? idmatricula)
        {

            Matricula objMatriculaSelecionada = new Matricula();
            double valorTotalContratado=0;
            List<MatriculaCurso> listMatriculaCurso = new List<MatriculaCurso>();
            objMatriculaSelecionada = db.matriculas.Where(x => x.MatriculaId == idmatricula).FirstOrDefault();
            listMatriculaCurso = db.matriculacurso.Where(x => x.MatriculaId == idmatricula).ToList();
            List<Curso> listaCursos = new List<Curso>();


            foreach(var item in listMatriculaCurso)
            {
                MatriculaCurso objMatCurso = new MatriculaCurso();
                Curso objCurso = new Curso();
                objMatCurso = db.matriculacurso.Where(x => x.MatriculaCursoId == item.MatriculaCursoId).FirstOrDefault();
                objCurso = db.cursos.Where(x => x.CursoId == objMatCurso.CursoId).FirstOrDefault();
                listaCursos.Add(objCurso);
                valorTotalContratado = valorTotalContratado + objCurso.Valor;

            }

            PagamentoMatriculaView objMatriculaView = new PagamentoMatriculaView();
            objMatriculaView.ValorPacote = valorTotalContratado;
            objMatriculaView.MatriculaId = objMatriculaSelecionada.MatriculaId;
            objMatriculaView.listaCursosMatriculados = listaCursos.OrderBy(x => x.NomeCurso).ToList();
            objMatriculaView.dataMatricula = objMatriculaSelecionada.DataMatricula;

            Pagamento objPagmenot = new Pagamento();
            objPagmenot = db.pagamentos.Where(x => x.MatriculaId == idmatricula).FirstOrDefault();

            if(objPagmenot != null)
            {
                objMatriculaView.listaPagamentoMensalidade = db.pagamentomensalidades.Where(x => x.PagamentoId == objPagmenot.PagamentoId).ToList();
            }
           
            

            return View("Mensalidade", objMatriculaView); 


        }



        // Excluir Mensalidade por Pagamento

        public ActionResult ExcluirPag(int? id)
        {
            if(id !=null)
            {
                PagamentoMensalidade objExcluir = new PagamentoMensalidade();
                int ?MatriculaId;
                
                objExcluir = db.pagamentomensalidades.Where(x => x.IdPagamentoMensalidade == id).FirstOrDefault();
                MatriculaId = objExcluir.Pagamento.Matricula.MatriculaId;

                db.pagamentomensalidades.Remove(objExcluir);
                db.SaveChanges();
                return RedirectToAction("Contrato", new { idmatricula = MatriculaId });

            }
            else
            {
                return Json(new { result = false });
            }
        }



    

        public ActionResult DetalhePagamento(int? AlunoId)
        {

            Aluno aluno = new Aluno();
            Pagamento objPagamento = new Pagamento();
            PagamentoMatriculaView objPagamentoMatriculaView = new PagamentoMatriculaView();

            List<Matricula> listaMatriculasRealizadas = new  List<Matricula>();

            listaMatriculasRealizadas = db.matriculas.Where(x => x.AlunoId == AlunoId).ToList();
               
            aluno = db.alunos.Where(x => x.AlunoId == AlunoId).FirstOrDefault();
            int alunoid;

            return null;
        }

        public ActionResult SalvarPagamento(int? id)
        {

            PagamentoMensalidade objPagamento = new PagamentoMensalidade();
            objPagamento = db.pagamentomensalidades.Find(id);
      
            db.Entry(objPagamento).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");

        }


        public Boolean VerificarPagamentoJaExistente(Pagamento objPagamento)
        {
            bool result = false;
            List<Pagamento> ListaPagamentosExistentes = new List<Pagamento>();
            ListaPagamentosExistentes = db.pagamentos.Where(x => x.MatriculaId == objPagamento.MatriculaId).ToList();

            DateTime dataCriacaoPagamento = Convert.ToDateTime(objPagamento.DataCriacaoPagamento).Date;

            if (ListaPagamentosExistentes != null)
            {
                foreach (var item in ListaPagamentosExistentes)
                {
                    DateTime dataCriacaoPag = Convert.ToDateTime(item.DataCriacaoPagamento);
                    if (dataCriacaoPag == dataCriacaoPagamento)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        public JsonResult AtivarDesativarBotao(Pagamento dados)
        {
            var res = true;
            Pagamento objPagamento = new Pagamento();
            objPagamento = db.pagamentos.Where(x => x.MatriculaId == dados.MatriculaId).FirstOrDefault();
            var dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
            if(objPagamento != null)
            {
                res = true;
            }
            else
                res = false;
            return Json(res);
        }


        public Boolean VerificarPagamento(PagamentoMensalidade objPagamentoMensalidade)
        {
            bool result = false;
            List<PagamentoMensalidade> ListaPagamentosExistentes = new List<PagamentoMensalidade>();
            ListaPagamentosExistentes = db.pagamentomensalidades.Where(x => x.PagamentoId == objPagamentoMensalidade.PagamentoId).ToList();

            
            return result;
        }

        // Pagamentos a receber por aluno


    }
}































