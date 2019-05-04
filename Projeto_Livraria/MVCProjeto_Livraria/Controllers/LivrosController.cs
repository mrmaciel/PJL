using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProjeto_Livraria.Models;
using System.Net.Http;
using System.Web.Helpers;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using PagedList;

namespace MVCProjeto_Livraria.Controllers
{
    public class LivrosController : Controller
    {
        // GET: Livros
        public ActionResult Index(string sOrdem, string filtroCorrente, string procurarpor, string Busca, int? page)
        {

            ViewBag.CurrentSort = sOrdem;
            ViewBag.ISBN = String.IsNullOrEmpty(sOrdem) ? "ISBN_ASC" : "";
            ViewBag.NOME = sOrdem == "NOME" ? "NOME_DESC" : "NOME";
            ViewBag.AUTOR = sOrdem == "AUTOR" ? "AUTOR_DESC" : "AUTOR";
            ViewBag.PRECO = sOrdem == "PRECO" ? "PRECO_DESC" : "PRECO";
            ViewBag.DTPUBLIC = sOrdem == "DTPUBLIC" ? "DTPUBLIC_DESC" : "DTPUBLIC";


            if (Busca != null)
            {
                page = 1;
            }
            else
            {
                Busca = filtroCorrente;
            }

            ViewBag.CurrentFilter = Busca;

            IEnumerable<MvcLivroModel> livroLista;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Livros").Result;
            livroLista = response.Content.ReadAsAsync<IEnumerable<MvcLivroModel>>().Result;

            var oLivro = from s in livroLista
                         select s;

            if (!String.IsNullOrEmpty(Busca))
            {
                if (procurarpor == "ISBN")
                    oLivro = oLivro.Where(s => s.ISBNID.ToString().StartsWith(Busca));
                else if (procurarpor == "NOME")
                    oLivro = oLivro.Where(s => s.NOME.Contains(Busca));
                else if (procurarpor == "AUTOR")
                    oLivro = oLivro.Where(s => s.AUTOR.Contains(Busca));
                else if (procurarpor == "PRECO")
                    oLivro = oLivro.Where(s => s.PRECO.ToString().StartsWith(Busca));
                else if (procurarpor == "DTPUBLIC")
                    oLivro = oLivro.Where(s => s.DTPUBLIC.ToString().Contains(Busca));                    
                else
                    oLivro = oLivro.Where(s => s.DTPUBLIC.Equals(Busca));
            }

            switch (sOrdem)
            {
                case "ISBN_ASC":
                    oLivro = oLivro.OrderBy(s => s.ISBNID);
                    break;              
                case "NOME":
                    oLivro = oLivro.OrderBy(s => s.NOME);
                    break;
                case "NOME_DESC":
                    oLivro = oLivro.OrderByDescending(s => s.NOME);
                    break;
                case "AUTOR":
                    oLivro = oLivro.OrderBy(s => s.AUTOR);
                    break;
                case "AUTOR_DESC":
                    oLivro = oLivro.OrderByDescending(s => s.AUTOR);
                    break;
                case "PRECO":
                    oLivro = oLivro.OrderBy(s => s.PRECO);
                    break;
                case "PRECO_DESC":
                    oLivro = oLivro.OrderByDescending(s => s.PRECO);
                    break;
                case "DTPUBLIC":
                    oLivro = oLivro.OrderBy(s => s.DTPUBLIC);
                    break;
                case "DTPUBLIC_DESC":
                    oLivro = oLivro.OrderByDescending(s => s.DTPUBLIC);
                    break;
                default:
                    oLivro = oLivro.OrderByDescending(s => s.ISBNID);
                    break;
            }


            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(oLivro.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(long id = 0)
        {
            
            //GetLivro(long id)
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Livros/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<MvcLivroModel>().Result);

        }

        public ActionResult AddorEdit()
        {           
           return View(new MvcLivroModel());  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit(MvcLivroModel livro)
        {

            var imageTypes = new string[]{
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

            HttpPostedFileBase FileBase = Request.Files[0];
            
            if ((FileBase != null) && !string.IsNullOrEmpty(FileBase.FileName))
            {

                WebImage imagemCapa = new WebImage(FileBase.InputStream);

                livro.IMGCAPA = imagemCapa.GetBytes();

                if (!imageTypes.Contains(FileBase.ContentType))

                {
                    ModelState.AddModelError("IMGCAPA", "Somente serão aceitos arquivos com a extensão GIF, JPG ou PNG.");
                    return View(livro);
                }
            }            
        

            
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Livros", livro).Result;
                            
                if (response.StatusCode == HttpStatusCode.Conflict)
                {

                    ModelState.AddModelError("ISBNID", "Este ISBN já existe.");
                    return View(livro);
                }


            }
            else
            {
                ModelState.AddModelError("Livros", "Erro ao Gravar as informações.");
                return View(livro);
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(long id = 0)
        {

            //GetLivro(long id)
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Livros/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<MvcLivroModel>().Result);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Livros/" + id).Result;
                      
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("ISBNID", "Falha ao excluir ISBN inexistente.");
                return View();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit(long id = 0)
        {

            //GetLivro(long id)
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Livros/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<MvcLivroModel>().Result);
                       
        }

        [HttpPost]
        public ActionResult Edit(MvcLivroModel livro)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Livros/" + livro.ISBNID.ToString()).Result;
            MvcLivroModel oLivro = response.Content.ReadAsAsync<MvcLivroModel>().Result;

            var imageTypes = new string[]{
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

            HttpPostedFileBase FileBase = Request.Files[0];

            if ((FileBase != null) && !string.IsNullOrEmpty(FileBase.FileName))
            {

                
                if (imageTypes.Contains(FileBase.ContentType))
                {
                    WebImage imagemCapa = new WebImage(FileBase.InputStream);
                    livro.IMGCAPA = imagemCapa.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("IMGCAPA", "Somente serão aceitos arquivos com a extensão GIF, JPG ou PNG.");
                    return View(livro);
                }
            }

            else if (oLivro.IMGCAPA != null)
            {

                livro.IMGCAPA = oLivro.IMGCAPA;
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.PutAsJsonAsync("Livros/" + livro.ISBNID, livro).Result;                
            }
            else
            {
                ModelState.AddModelError("livro", "Erro ao gravar a edição do livro.");
                return View(livro);
            }

            return RedirectToAction("Index");
        }

        
    }
       
}