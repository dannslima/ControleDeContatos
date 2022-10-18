using ControleDeContatos.Models;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {

        UsuarioModel ListarPorId(int id);

        UsuarioModel? BuscarPorLogin(string login);
        UsuarioModel? BuscarPorEmailELogin(string email, string login);

        UsuarioModel? BuscarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Apagar(int usuario);
    }
}

