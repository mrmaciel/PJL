using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ClassLibrary_Projeto_Livraria.Entidade
{
    public partial class Livro
    {
        [ScaffoldColumnAttribute(true)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Informe o ISBN.")]
        [Display(Name = "ISBN")]
        [Range(1, 9999999999999, ErrorMessage = "O ISBN deve estar entre 1 e 9999999999999.")]
        public long ISBNID { get; set; }

        [Required(ErrorMessage = "Informe o Autor do Livro.")]
        [MaxLength(100)]
        public string AUTOR { get; set; }
        
        [Required(ErrorMessage = "Informe o Título do Livro.")]
        [MaxLength(100)]
        public string NOME { get; set; }

        [Required(ErrorMessage = "Informe o Preço do Livro.")]
        [Display(Name = "PREÇO")]
        [DisplayFormat(DataFormatString = "{0,c2}")]
        public decimal PRECO { get; set; }
               
        [Required(ErrorMessage = "Informe a Data da Publicação.")]
        [Display(Name = "DATA DA PUBLICAÇÃO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DTPUBLIC { get; set; }               

        public byte[] IMGCAPA { get; set; }
    }
}
