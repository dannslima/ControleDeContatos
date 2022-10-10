using ControleDeContatos.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o Nome do usuario")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Login do usuario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o Email do usuario")]
        [EmailAddress(ErrorMessage = "O email não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o Perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }

        
    }
}
