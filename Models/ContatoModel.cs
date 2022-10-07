using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Digite o Nome do Contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Digite o Email do contato")]
        [EmailAddress(ErrorMessage ="O email não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Digite o Celular do contato")]
        [Phone(ErrorMessage ="O celular informado não é válido")]
        public string Celular { get; set; }

    }
}
