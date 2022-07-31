using bp_login.data.Context;
using bp_login.domain.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bp_login.data.Repo
{
    public class LoginRepo : ILoginRepo
    {
        private readonly IBDContext bd;
        public LoginRepo(IBDContext bd) { this.bd = bd; }

        //Retorna um lista de logins a serem listados na View: "Index"
        public IEnumerable<Login> getLoginUsuarios() =>
            bd.data().Query<Login>("SELECT L.login, nome, data_aniversario, ultimaSessao " +
                "FROM usuario_login L JOIN usuario_info I ON L.id = I.login");

        //Confirma a existência do usuário no banco e retorna suas informações
        public Login loginUsuario(LoginRequest request)
        {
            try
            {
                string sql = "select * from usuario_login WHERE login = @login AND senha = @senha";
                object obj = new { login = request.login, senha = request.senha };
                if (bd.data().Query(sql, obj).Count() == 1)
                {
                    bd.data().Query("UPDATE usuario_info SET ultimaSessao = @data " +
                        "WHERE login = (SELECT id FROM usuario_login WHERE login = @login)",
                        new { data = DateTime.Now, login = request.login });

                    return bd.data().Query<Login>(
                        " SELECT L.login, nome, data_aniversario, ultimaSessao " +
                        " FROM usuario_login L JOIN usuario_info I ON L.id = I.login " +
                        " WHERE L.login = @login", new { login = request.login }).First();
                }
                return default;
            }
            catch (Exception)
            { return default; }
        }

        //Realiza a alteração dos dados do usuário no banco de dados
        public object alteracao(AlteracaoRequest request, string login)
        {
            try
            {
                if (request.action == "Nome")
                {
                    bd.data().Query("UPDATE usuario_info SET nome = @nome " +
                        " WHERE login = (SELECT id FROM usuario_login WHERE login = @login)",
                        new { nome = request.nome, login = login });
                    return new { valid = true, message = "Alteração concluída com sucesso" };
                }
                else if (request.action == "Aniversario")
                {
                    if (request.data > DateTime.Now)
                    { return new { valid = false, message = "Data de nascimento maior que a data atual " }; }

                    bd.data().Query("UPDATE usuario_info SET data_aniversario = @data " +
                        " WHERE login = (SELECT id FROM usuario_login WHERE login = @login)",
                        new { data = request.data.Value.ToString("yyyy-MM-dd"), login = login });
                    return new { valid = true, message = "Alteração concluída com sucesso" };
                }
                else
                {
                    return new { valid = false, message = "Ação não designada corretamente" };
                }
            }
            catch (Exception e) { return new { valid = false, message = e.Message }; }
        }

        //Retorna as informações do usuário registrado na sessão
        public Login renovaSessao(string login) => bd.data().Query<Login>(
            "SELECT L.login, nome, data_aniversario, ultimaSessao " +
            "FROM usuario_login L JOIN usuario_info I ON L.id = I.login " +
            " WHERE L.login = @login", 
            new {login = login}).First();

        //Registra o usuário no banco de dados
        public object registraUsuario(LoginRegistroRequest request)
        {
            try
            {
                string sql = "SELECT * FROM usuario_login WHERE login = @login";
                object obj = new { login = request.login };

                if (bd.data().Query(sql, obj).Count() != 0)
                { return new { valid = false, message = "Já existe alguém utilizando este login" }; }

                if (request.data_aniversario >= DateTime.Now)
                { return new { valid = false, message = "Data não pode ser a mesma ou maior que a data atual" }; }

                bd.data().Query("INSERT INTO usuario_login (login, senha) VALUES (@login, @senha)",
                    new { login = request.login, senha = request.senha });

                bd.data().Query("INSERT INTO usuario_info (nome, data_aniversario, login) VALUES (@nome, @dataAniversario, " +
                    " (SELECT id FROM usuario_login WHERE login = @login))",
                    new { nome = request.nome, dataAniversario = request.data_aniversario, login = request.login });

                return new { valid = true, message = "Usuário registrado com sucesso" };
            }
            catch (Exception e) { return new { valid = false, message = e.Message }; }
        }

        //Remove o usuário no banco de dados
        public object removerUsuario(string login)
        {
            try
            {
                bd.data().Query("DELETE FROM usuario_login WHERE login = @login", new { login = login });
                return new { valid = true, message = "Removido com sucesso" };
            }
            catch (Exception e)
            { return new { valid = false, message = e.Message }; }
        }
    }
}
