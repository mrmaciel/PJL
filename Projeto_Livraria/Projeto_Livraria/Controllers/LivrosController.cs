using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClassLibrary_Projeto_Livraria.Contexto;
using ClassLibrary_Projeto_Livraria.Entidade;
using ClassLibrary_Projeto_Livraria.Interface;
using Ninject;



namespace Projeto_Livraria.Controllers
{
    public class LivrosController : ApiController
    {
        

        private ILivro _repositorio;

        public LivrosController(ILivro repositorio)
        {
            _repositorio = repositorio;            
        }

        
        // GET: api/Livros
        public List<Livro> GetLivros()
        {
            return _repositorio.GetAll();
        }

        // GET: api/Livros/5
        [ResponseType(typeof(Livro))]
        public IHttpActionResult GetLivro(long id)
        {
            Livro livro = _repositorio.Find(id);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        // PUT: api/Livros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLivro(long id, Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro.ISBNID)
            {
                return BadRequest();
            }


            try
            {
                   
                _repositorio.Update(livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Livros
        [ResponseType(typeof(Livro))]
        public IHttpActionResult PostLivro(Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {

                _repositorio.Add(livro);

            }
            catch (DbUpdateException)
            {
                if (LivroExists(livro.ISBNID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = livro.ISBNID }, livro);
        }

        // DELETE: api/Livros/5
        [ResponseType(typeof(Livro))]
        public IHttpActionResult DeleteLivro(long id)
        {
            Livro livro = _repositorio.Find(id);

            if (livro == null)
            {
                return NotFound();
            }

            _repositorio.Remove(livro);
            

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositorio.Dispose(disposing);
            }
            base.Dispose(disposing);
        }

        private bool LivroExists(long id)
        {
            
            Livro livro = _repositorio.Find(id);

            if (livro == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}