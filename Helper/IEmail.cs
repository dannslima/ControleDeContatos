﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Helper
{
    public interface IEmail
    {
         public bool Enviar(string email, string assunto, string mensagem);
    }
}
