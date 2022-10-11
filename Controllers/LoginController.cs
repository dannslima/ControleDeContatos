using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public LoginController(IUsuarioRepositorio _usuarioRepositori)
        {
            _usuarioRepositorio = _usuarioRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   UsuarioModel usuario =  _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                            TempData["MensagemErro"] = $" Ops, Senha inválida";

                    }

                    TempData["MensagemErro"] = $" Ops, Não conseguimos validar seu login. Tente novamente";

                }

                return View("Index");
            }
            catch(Exception erro)
            {
                TempData["MensagemErro"] = $" Ops, Não conseguimos validar seu login. Tente novamente{erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
