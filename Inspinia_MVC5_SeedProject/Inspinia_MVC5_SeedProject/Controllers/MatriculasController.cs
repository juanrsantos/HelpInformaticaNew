using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class MatriculasController : Controller
    {


        private Contexto db = new Contexto();

        // GET: Matriculas
        public async Task<ActionResult> Index(string nome)
        {

            MatriculasAtivasView objMatriculaView = new MatriculasAtivasView(); 
            Matricula objMatr = new Matricula();
            IList<Matricula> listaMatriculasAtivas = new List<Matricula>();
            objMatriculaView.listaMatriculasAtivasView = db.matriculas.ToList().OrderBy(x => x.DataMatricula).ToList();
            objMatr.objMatriculaAtivaView = objMatriculaView;
            return View(objMatr);
        }


        // GET
        public ActionResult Create(int? idMatricula)
        {
            Matricula objMat = new Matricula();
            List<MatriculaCurso> listaCursosMatriculados = new List<MatriculaCurso>();

            MatriculaCursoView objMatriculaCursoView = new MatriculaCursoView();
            List<Curso> lCursosOfertados = new List<Curso>();
            List<Curso> listaCursosContratados = new List<Curso>();


            listaCursosMatriculados = db.matriculacurso.Where(x => x.Matricula.MatriculaId == idMatricula).ToList();

            foreach (var item in listaCursosMatriculados)
            {
                Curso objCurso = new Curso();
                objCurso = db.cursos.Where(x => x.CursoId == item.CursoId).FirstOrDefault();
                listaCursosContratados.Add(objCurso);
            }



            objMatriculaCursoView.listaCursosOfertados = db.cursos.OrderBy(x => x.NomeCurso).ToList();
            objMatriculaCursoView.listaCursosMatriculados = listaCursosContratados;

            objMat.objMatView = objMatriculaCursoView;
            if (idMatricula != null)
            {
                objMat.MatriculaId = (int)idMatricula;
            }



            return View(objMat);
        }




        [HttpPost]
        public JsonResult Create(List<MatriculaCurso> list)
        {
            MatriculaCurso objMatriculaCurso;
            Pagamento objPagamentoExistente = new Pagamento();
            List<MatriculaCurso> listaMatriculaCurso = new List<MatriculaCurso>();
            bool verificar;
            bool vPagamentoGerado;
          
            //int? MatId = 0;
            try
            {

               
      
                if (list != null)
                {
                    foreach (var item in list)
                    {

                        objMatriculaCurso = new MatriculaCurso();
                        verificar = verificarCursoCadastrado(item.MatriculaId, item.CursoId);

                        if (!verificar)
                        {
                            vPagamentoGerado = verificarPagamentoGerado(item.MatriculaId);
                            if (vPagamentoGerado == false)
                            {
                                objMatriculaCurso.CursoId = item.CursoId;
                                objMatriculaCurso.MatriculaId = item.MatriculaId;
                                // MatId = objMatriculaCurso.MatriculaId;
                                listaMatriculaCurso.Add(objMatriculaCurso);
                                db.matriculacurso.Add(objMatriculaCurso);
                                db.SaveChanges();
                                TempData["success"] = "MATRICULADO COM SUCESSO";
                            }
                        }
                   
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Erro ao Matricular Aluno";

            }
            return Json(new {result=true});
        }


        [HttpPost]
        public JsonResult ExcluirMatriculaCurso(int? idMatricula)
        {
            MatriculaCurso objMatriculaCurso = new MatriculaCurso();
            if (idMatricula != null)
            {
                objMatriculaCurso = db.matriculacurso.Where(x => x.MatriculaId == idMatricula).FirstOrDefault();
                if (objMatriculaCurso != null)
                {
                    db.matriculacurso.Remove(objMatriculaCurso);
                    db.SaveChanges();
                }
            }
            return Json(new { success = true });
        }



        public Boolean verificarPagamentoGerado(int? idmatricula)
        {
            Pagamento objPagamento = new Pagamento();
            objPagamento = db.pagamentos.Where(x => x.MatriculaId == idmatricula).FirstOrDefault();

            if(objPagamento != null)
            {

                TempData["warning"] = "JÁ EXISTE PAGAMENTO GERADO PARA ESTA MATRICULA";
                TempData["info"] = "FAVOR GERAR UMA NOVA";
                return true;
              
            }
            return false;

        }

        public Boolean verificarCursoCadastrado(int? MatriculaId, int? idcurso)
        {

            List<MatriculaCurso> listaMatriculaCurso = new List<MatriculaCurso>();
            listaMatriculaCurso = db.matriculacurso.Where(x => x.MatriculaId == MatriculaId).ToList();
            bool resultado = false;

            if (listaMatriculaCurso != null)
            {
                foreach (var item in listaMatriculaCurso)
                {
                    if (item.CursoId == idcurso)
                    {
                        TempData["warning"] = "CURSO JÁ LANÇADO";
                        resultado = true;
                    }
                }
            }

            return resultado;
        }

        public List<Curso> retornaListaCursos()
        {
            db.cursos.ToList();
            return db.cursos.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }


}