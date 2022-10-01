using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }
        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if(contatoDB == null) throw new System.Exception("hovue um erro na atualizaçaõ do contato");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _bancoContext.Update(contatoDB);
            _bancoContext.SaveChanges();
            return contatoDB;
        }

        public bool  Apagar(int contato)
        {
            ContatoModel contatoDB = ListarPorId(contato);
            if (contatoDB == null) throw new System.Exception("hovue um erro na deleção do contato");

             _bancoContext.Remove(contatoDB);
            _bancoContext.SaveChanges();
            return true;
        }

    }
}
