using bp_login.domain.Models;

namespace bp_login.data.Repo
{
    public interface ILoginRepo
    {
        IEnumerable<Login> getLoginUsuarios();
        Login loginUsuario(LoginRequest request);
        Login renovaSessao(string login);
        object alteracao(AlteracaoRequest request, string login);
        object registraUsuario(LoginRegistroRequest request);
        object removerUsuario(string login);
    }
}