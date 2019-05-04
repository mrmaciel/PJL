using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_Projeto_Livraria.Contexto;
using ClassLibrary_Projeto_Livraria.Entidade;
using ClassLibrary_Projeto_Livraria.Interface;

namespace ClassLibrary_Projeto_Livraria.Repositorio
{
    public class LivroRepositorio : ILivro
    {
        
        private EFDBContexto Contexto { get; set; }
               

        public List<Livro> GetAll()
        {
            Contexto = new EFDBContexto();
            IQueryable<Livro> consultaCliente = Contexto.Livros.AsQueryable<Livro>();
            return Contexto.Livros.ToList();

        }

       

        public Livro Add(Livro item)
        {

            using (Contexto = new EFDBContexto())
            {

                Contexto.Livros.Add(item);
                Contexto.SaveChanges();
                return item;
            }

        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Contexto != null)
                {
                    Contexto.Dispose();
                    Contexto = null;
                }
            }
        }

        public Livro Find(long id)
        {
            using (Contexto = new EFDBContexto())
            {
                return Contexto.Livros.Find(id);
            }
        }

        public void Remove(Livro item)
        {
            using (Contexto = new EFDBContexto())
            {

                Livro oLivroItem = Contexto.Livros.Find(item.ISBNID);

                Contexto.Livros.Remove(oLivroItem);
                Contexto.SaveChanges();
            }
        }

        public void Update(Livro item)
        {
            using (Contexto = new EFDBContexto())
            {
                Contexto.Entry<Livro>(item).State = EntityState.Modified;
                Contexto.SaveChanges();
            }

        }

        
    }
}
