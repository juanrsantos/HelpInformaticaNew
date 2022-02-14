



using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class AlunosController : Controller
    {
        private Contexto db = new Contexto();
        public IList<Curso> carregarDropList;





        // STATUS 0 ATIVO / 1 INATIVO


        // GET: /Alunos/
        public async Task<ActionResult> Index(string nome)
        {

            if (nome == null)
                return View(await db.alunos.Where(x => x.status == 0).ToListAsync());
            return View(await db.alunos.Where(x => x.Nome.Contains(nome) && x.status == 0).ToListAsync());

        }

        // GET: /Alunos/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            int idaluno = (int)id;
            IList<CursosMatriculados> listaCursosMatriculadosView = new List<CursosMatriculados>();
            List<MatriculasAtivasView> listaMAtivas = new List<MatriculasAtivasView>();
            IList<Matricula> listaMatriculasId = new List<Matricula>();


            carregarDropList = obterListaCurso();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aluno aluno = await db.alunos.FindAsync(id);

            aluno.listaCursos = carregarDropList.OrderBy(x => x.NomeCurso).ToList();
            listaMatriculasId = db.matriculas.Where(x => x.AlunoId == id).ToList();

            if (listaMatriculasId != null)
            {
                MatriculasAtivasView objMatriculaAtivas;
                foreach (var item in listaMatriculasId)
                {
                    objMatriculaAtivas = new MatriculasAtivasView();
                    objMatriculaAtivas.MatriculaId = item.MatriculaId;
                    objMatriculaAtivas.DataMatricula = item.DataMatricula;
                    listaMAtivas.Add(objMatriculaAtivas);

                }

            }

            aluno.listaMatriculasAtivas = listaMAtivas;


            //  aluno.listaCursosMatriculados = listaCursosMatriculadosView;

            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // GET: /Alunos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Alunos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aluno aluno, HttpPostedFileBase file)
        {

            Aluno novoAluno = new Aluno();
            Aluno alunoBanco = new Aluno();


            try
            {



                if (ModelState.IsValid)
                {
                    tratarDados tratarAluno = new tratarDados();
                    novoAluno = tratarAluno.tratarDadosAluno(aluno);


                    alunoBanco = db.alunos.Where(x => x.Cpf == novoAluno.Cpf).FirstOrDefault();
                    if (alunoBanco != null)

                    {
                        TempData["warning"] = "CPF já Existente";

                    }
                    else
                    {
                        aluno.status = 0;
                        aluno.DataAlteracao = DateTime.Now;
                        novoAluno = tratarAluno.tratarDadosAluno(novoAluno);

                        db.alunos.Add(novoAluno);

                        if (file != null)
                        {
                            String[] strName = file.FileName.Split('.');
                            String strExt = strName[strName.Count() - 1];
                            string pathSave = String.Format("{0}{1}.{2}", Server.MapPath("~/img/"), aluno.AlunoId, strExt);
                            String pathBase = String.Format("/img/{0}.{1}", aluno.AlunoId, strExt);
                            file.SaveAs(pathSave);
                            novoAluno.Foto = pathBase;
                            db.SaveChanges();
                        }
                        TempData["success"] = "Cadastrado com Sucesso";
                        return RedirectToAction("Index");
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                }
            }
            return View(aluno);
        }





        // GET: /Alunos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = await db.alunos.FindAsync(id);
            if (aluno.Foto != null)
            {
                String[] strName = aluno.Foto.Split('/');
            }


            //  aluno.Foto = strName[2];


            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: /Alunos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Aluno aluno, HttpPostedFileBase file)
        {



            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
                if (file != null)
                {
                    if (aluno.Foto != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/" + aluno.Foto)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/" + aluno.Foto));
                        }
                    }

                    aluno.DataAlteracao = DateTime.Now;
                    String[] strName = file.FileName.Split('.');
                    String strExt = strName[strName.Count() - 1];
                    string pathSave = String.Format("{0}{1}.{2}", Server.MapPath("~/img/"), aluno.AlunoId, strExt);
                    String pathBase = String.Format("/img/{0}.{1}", aluno.AlunoId, strExt);
                    file.SaveAs(pathSave);
                    aluno.Foto = pathBase;




                    db.SaveChanges();


                    TempData["success"] = "Alterado com Sucesso";
                }
                return RedirectToAction("Index");

            }
            return View(aluno);
        }

        // GET: /Alunos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = await db.alunos.FindAsync(id);


            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }



        // 0 ATIVO / 1 Inativo
        // POST: /Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            Aluno aluno = await db.alunos.FindAsync(id);
            aluno.DataExclusao = DateTime.Now;
            aluno.status = 1;

            TempData["success"] = "Excluido com Sucesso";
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
            {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region[METODOS INTERNOS]

        public string obterCaminho(int id)
        {

            Aluno aluno = db.alunos.Where(x => x.AlunoId == id).FirstOrDefault();
            string caminho = aluno.Foto;
            return caminho;
        }





        public void ExcluirPagamento(int MatriculaID)
        {

            Matricula Matricula = new Matricula();

            Pagamento pagamento = new Pagamento();

            /*   pagamento = db.pagamentos.Where(x => x.MatriculaId == MatriculaID).FirstOrDefault();
                Matricula = db.matriculas.Where(x => x.MatriculaId == MatriculaID).FirstOrDefault();
                MCurso = db.matriculascursos.Where(x => x.MatriculaCursoId == Matricula.MatriculaCursoId).FirstOrDefault();
                pagamento.matricula = Matricula;
                pagamento.matricula.MatriculaCurso = MCurso;

                db.pagamentos.Remove(pagamento);
                db.matriculascursos.Remove(MCurso);
                db.matriculas.Remove(Matricula);*/


        }

        //public int RetornarIDAluno(int MatriculaID)
        //{
        //    int? idAluno = 0;

        //    Matricula matricula = new Matricula();

        //    using (Contexto con = new Contexto())
        //    {

        //        matricula = con.matriculas.Where(x => x.MatriculaId == MatriculaID).FirstOrDefault();
        //        idAluno = matricula.Aluno.AlunoId;

        //    }

        //    return int.Parse(idAluno.ToString());

        //}

        //public Matricula RetornarMatricula(int MatriculaCursoID)
        //{

        //    Matricula matricula = new Matricula();
        //    matricula = db.matriculas.Where(x => x.MatriculaCursoId == MatriculaCursoID).FirstOrDefault();
        //    return matricula;
        //}


        //public void ExcluirMatriculaCurso2(Matricula matricula)
        //{
        //    MatriculaCurso matriculaCurso = new MatriculaCurso();
        //    Curso curso = new Curso();

        //    //using (Contexto con = new Contexto())
        //    // {
        //    matriculaCurso = db.matriculascursos.Where(x => x.MatriculaCursoId ==matricula.MatriculaId).FirstOrDefault();
        //    curso = db.cursos.Where(x => x.CursoId == matriculaCurso.CursoId).FirstOrDefault();
        //    matriculaCurso.Curso = curso;
        //    db.matriculascursos.Remove(matriculaCurso);

        //    //}

        //}


        public async Task<ActionResult> ExcluirMatricula(int? idMatricula)

        {
            Matricula matricula = new Matricula();
            int? id;
            Aluno aluno = new Aluno();



            matricula = db.matriculas.Where(x => x.MatriculaId == idMatricula).FirstOrDefault();
            id = matricula.AlunoId;

            db.matriculas.Remove(matricula);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = id });

        }

        public IList<Curso> obterListaCurso()
        {

            IList<Curso> listaCurso = new List<Curso>();
            IList<Curso> listaCursoView = new List<Curso>();

            listaCurso = db.cursos.ToList();

            foreach (var item in listaCurso)
            {

                Curso objCurso = new Curso()
                {
                    CursoId = item.CursoId,
                    NomeCurso = item.NomeCurso
                };

                listaCursoView.Add(objCurso);

            }

            return listaCursoView.OrderBy(x => x.NomeCurso).ToList();
        }

        //public ActionResult obterDiaSemana(string IdAluno)

        //{
        //    int id=0;

        //    if(IdAluno != null)
        //    {
        //        id = int.Parse(IdAluno);
        //    }


        //    Matricula objMatricula = new Matricula();
        //    MatriculaCurso objMatriculaCurso = new MatriculaCurso();
        //    objMatricula = db.matriculas.Where(x => x.Aluno.AlunoId == id).FirstOrDefault();

        //    if(objMatricula != null)
        //    {
        //        objMatriculaCurso = db.matriculascursos.Where(x => x.MatriculaCursoId == objMatricula.MatriculaCursoId).FirstOrDefault();

        //    }

        //    var resultado = new
        //    {
        //        PrimeiroDia = objMatriculaCurso.PrimeiroDia,
        //        SegundoDia = objMatriculaCurso.SegundoDia,

        //    };


        //    return Json(resultado, JsonRequestBehavior.AllowGet);
        //}




        public ActionResult obterCurso(string idCurso)
        {

            int CursoId;
            CursoId = int.Parse(idCurso);
            Curso Cselecionado = new Curso();

            Cselecionado = db.cursos.Where(x => x.CursoId == CursoId).FirstOrDefault();

            var resultado = new
            {
                nome = Cselecionado.NomeCurso.ToString(),
                periodo = Cselecionado.Periodo.ToString(),
                valor = Cselecionado.Valor.ToString(),
                descricao = Cselecionado.Descricao.ToString()


            };


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public void ValidarCpfExistente(Aluno aluno)
        {

            Aluno novoAluno = new Aluno();
            Aluno alunoBanco = new Aluno();
            novoAluno = aluno;

            alunoBanco = db.alunos.Where(x => x.Cpf == novoAluno.Cpf).FirstOrDefault();

            if (alunoBanco != null)

            {
                TempData["warning"] = "CPF JÁ EXISTENTE";

            }

        }






        //public Matricula PesquisaSeExistePagamentoMatricula(int idAluno)
        //{
        //    Matricula m = new Matricula();

        //    using (Contexto cont = new Contexto())
        //    {
        //        m = cont.matriculas.Where(x => x.Aluno.AlunoId == idAluno).OrderByDescending(x => x.MatriculaId).FirstOrDefault();
        //    }

        //    return m;

        //}

        /* public Pagamento PesquisaSeExisteDataPagamento(int matriculaID)
         {
             Pagamento m = new Pagamento();

             using (Contexto cont = new Contexto())
             {
                 m = cont.pagamentos.Where(x => x.MatriculaId == matriculaID).OrderByDescending(o => o.PagamentoId).FirstOrDefault();
             }

             return m;

         }*/




        public ActionResult Inativos()
        {

            IList<Aluno> listaAlunosInativos = new List<Aluno>();
            listaAlunosInativos = db.alunos.Where(x => x.status == 1).ToList();
            listaAlunosInativos.OrderBy(x => x.Nome).ToList();

            return View("AlunosInativos", listaAlunosInativos);

        }







        public string gerarNumeroMatricula(Aluno aluno)
        {
            string cpf = aluno.Cpf;
            // String dataatual = DateTime.Now.ToString("yyyy'-'MM'-'dd");

            DateTime dataatual = DateTime.Now;
            int dia = dataatual.Day;
            int minutos = dataatual.Minute;
            int segundos = dataatual.Second;

            string ngerado = cpf + dia + minutos + segundos;




            return ngerado;
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


            return  RedirectToAction("Details", new { id = idAluno });

        }



        public JsonResult AtivarAluno(int? idAluno)
        {
            int? AlunoId;
            Aluno aluno = new Aluno();

            if (idAluno != 0 || idAluno != null)
            {
                AlunoId = idAluno;
            }

            aluno = db.alunos.Where(x => x.AlunoId == idAluno).FirstOrDefault();
            aluno.status = 0;

            db.Entry(aluno).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true });
        }



        #endregion

    }


}

