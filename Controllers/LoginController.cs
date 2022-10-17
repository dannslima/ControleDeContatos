using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                               ISessao sessao,
                               IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            // Se usuario estiver logado, redirecionar para a home

            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
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
                            _sessao.CriarSessaoDoUsuario(usuario);
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

        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        
                        string mensagem = $"sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email,"Sistema de Contatos - Redefinir Senha", mensagem );

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $" Enviamos para seu e-mail cadastrado uma nova senha";
                        }

                        else
                        {
                            TempData["MensagemErro"] = $" Ops, Não conseguimos enviar o e-mail. Tente novamente";
                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $" Ops, Não conseguimos Definir sua senha. Verifique os dados informados";

                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $" Ops, Não conseguimos redefinir sua sneha. Tente novamente{erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
