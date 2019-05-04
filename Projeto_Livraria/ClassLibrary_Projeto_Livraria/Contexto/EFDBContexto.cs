using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_Projeto_Livraria.Entidade;

namespace ClassLibrary_Projeto_Livraria.Contexto
{
    public class EFDBContexto: DbContext
    {
        public DbSet<Livro> Livros { get; set; }

    }
}
