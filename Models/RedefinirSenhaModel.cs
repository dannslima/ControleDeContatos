using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail")]
        public string Email { get; set; }
    }
}
