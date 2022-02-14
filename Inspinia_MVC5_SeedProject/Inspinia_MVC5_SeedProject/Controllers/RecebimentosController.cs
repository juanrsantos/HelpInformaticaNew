using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class RecebimentosController:Controller
    {
   
        private Contexto db = new Contexto();



        // GET: Recebimentos Lista todos em abertos 
        public async Task<ActionResult> Index(string nome)
        {

            List<Matricula> listaMatriculasAtivas = new List<Matricula>();
            List<MatriculaCursoView> listMatriculaView = new List<MatriculaCursoView>();
            List<MatriculaCurso> listMatriculaCurso = new List<MatriculaCurso>();


            if (nome == null || nome == "")
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

                foreach (var item in listaMatriculasAtivas)
                {
                    if (item.Aluno.Nome.Contains(nome))
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



        // GET: Recebimentos Lista por aluno
        public ActionResult PagamentosAberto(int? id)
        {
            
            Pagamento objPagamento = new Pagamento();
            objPagamento = db.pagamentos.Where(x => x.MatriculaId == id).FirstOrDefault();
            List<PagamentoMensalidade> listaRecebimentoPorId = new List<PagamentoMensalidade>();
            listaRecebimentoPorId = db.pagamentomensalidades.Where(x => x.PagamentoId == objPagamento.PagamentoId).ToList().OrderBy(x => x.DataVencimento).ToList();
            List<RecebimentoView> listaRecebimentoView = new List<RecebimentoView>();

            foreach(var item in listaRecebimentoPorId)
            {
                RecebimentoView objRecebimnetoView = new RecebimentoView();
                objRecebimnetoView.DataVencimento = item.DataVencimento;
                objRecebimnetoView.DataPagamento = item.DataPagamento;
                objRecebimnetoView.ValorParcela = item.ValorPagamento;
                objRecebimnetoView.PagamentoMensalidadeId = item.IdPagamentoMensalidade;


                objRecebimnetoView.Status = verificarVencimento(item.DataVencimento, item.Pago);
                     
              
                listaRecebimentoView.Add(objRecebimnetoView);

            }
         
            return View("PagamentosAberto", listaRecebimentoView);
        }
        public Boolean? verificarVencimento (DateTime? DataVencimento, bool? Status)
        {
            bool? res = true;

            if (Status == null)
            {
                return res = null;

            }
            else if(Status == true)
            {
                return res = true;
            }
            else if (Status== null & DateTime.Now > DataVencimento)
            {
                res = false;

                return res;
            }
            return res;
        }
        public JsonResult AtivarAluno(int? idAluno)
        {
            int? PagMensalidadeId;
            PagamentoMensalidade objPagMensalidade = new PagamentoMensalidade();
                
            if (idAluno != 0 || idAluno != null)
            {
                PagMensalidadeId = idAluno;
                objPagMensalidade = db.pagamentomensalidades.Where(x => x.IdPagamentoMensalidade == PagMensalidadeId).FirstOrDefault();
            }
            objPagMensalidade.Pago = true;
            objPagMensalidade.DataPagamento = DateTime.Now;
     
            db.Entry(objPagMensalidade).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true });
        }

    }
}