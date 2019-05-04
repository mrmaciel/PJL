using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_Projeto_Livraria.Entidade;

namespace ClassLibrary_Projeto_Livraria.Interface
{
    public interface ILivro
    {
        //IQueryable<Livro> Livros { get; }

        List<Livro> GetAll();
        Livro Find(long id);
        Livro Add(Livro item);
        void Remove(Livro item);
        void Update(Livro item);
        void Dispose(bool disposing);
    }
}
