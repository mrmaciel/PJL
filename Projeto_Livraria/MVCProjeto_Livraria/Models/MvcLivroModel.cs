using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProjeto_Livraria.Models
{
    public class MvcLivroModel
    {

        [ScaffoldColumnAttribute(true)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Informe o ISBN.")]
        [Display(Name = "ISBN")]
        [Range(1, 9999999999999, ErrorMessage = "O ISBN deve estar entre 1 e 9999999999999.")]
        public long ISBNID { get; set; }

        [Required(ErrorMessage = "Informe o Autor do Livro.")]
        [MaxLength(100, ErrorMessage = "Comprimento máximo de 100 caracteres.")]
        public string AUTOR { get; set; }

        [Required(ErrorMessage = "Informe o Título do Livro.")]
        [MaxLength(100, ErrorMessage="Comprimento máximo de 100 caracteres.")]
        public string NOME { get; set; }

        [Required(ErrorMessage = "Informe o Preço do Livro.", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "PREÇO")]
        public decimal PRECO { get; set; }

        [Required(ErrorMessage = "Informe a Data da Publicação.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] 
        [Display(Name = "DATA DA PUBLICAÇÃO")]
        public DateTime DTPUBLIC { get; set; }

        [Display(Name = "CAPA")]
        public byte[] IMGCAPA { get; set; }
    }
}