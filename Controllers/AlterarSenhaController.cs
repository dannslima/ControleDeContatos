using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{
    public class AlterarSenhaController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio,
                                        ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar( AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();

                    UsuarioModel usuarioDB = _usuarioRepositorio.BuscarPorId(usuario.Id);
                    
                    if(usuarioDB == null)
                        throw new Exception("Usuario não encontrado");
                    
                    usuarioDB.Senha = usuarioDB.AtualizarSenha(alterarSenhaModel.NovaSenha);
                    usuarioDB.DataAtualizacao = DateTime.Now;
                    _usuarioRepositorio.Atualizar(usuarioDB);

                    TempData["MensagemSucesso"] = "Senha alterada com sucesso";
                }

                return View("Index", alterarSenhaModel);
                
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "ops, não conseguimosalterar a senha";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}
