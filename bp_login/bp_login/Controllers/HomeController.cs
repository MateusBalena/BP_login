using bp_login.data.Repo;
using bp_login.domain.Models;
using bp_login.Filtros;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace bp_login.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginRepo login;
        private readonly IHttpContextAccessor context;

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método construtor da classe, com todas as dependências necessárias
        public HomeController(ILogger<HomeController> logger, ILoginRepo login, IHttpContextAccessor context)
        {
            _logger = logger;
            this.login = login;
            this.context = context;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método para redirecionar à página principal
        public IActionResult Index() => View(login.getLoginUsuarios());

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método para redirecionar à página de registro do usuário
        public IActionResult registraUsuario() => View();

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método para requisitar o login do usuário
        public string logaUsuario([FromBody] LoginRequest request)
        {
            Login usuario = login.loginUsuario(request);
            if (usuario == default(Login))
            { return JsonConvert.SerializeObject(new { valid = false, message = "Login Incorreto" }); }
            logaUsuario(usuario);
            return JsonConvert.SerializeObject(new { valid = true, message = "Login feito com sucesso" });
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método privado (sem acesso para requisições HTTP) para registrar o usuário na sessão do ASP.NET CORE
        private void logaUsuario(Login login) =>
            context.HttpContext.Session.SetString("usuarioSessao", JsonConvert.SerializeObject(login));

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método para remover todas as entradas do usuário na sessão atual
        public IActionResult deslogaUsuario()
        {
            context.HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método requisitado por função AJAX (ou qualquer outro serviço de gerenciamento de requisições HTTP) para fazer alterações nos usuários cadastrados
        public string alteracao([FromBody] AlteracaoRequest request) =>
            JsonConvert.SerializeObject(
                login.alteracao(request,
                JsonConvert.DeserializeObject<Login>(context.HttpContext.Session.GetString("usuarioSessao")).login));

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método requisitado por função AJAX (ou qualquer outro serviço de gerenciamento de requisições HTTP) para renovar as informações do usuário registrada na sessão do ASP.NET CORE
        public string renovaSessao()
        {
            try
            {
                Login novoLogin = login.renovaSessao(JsonConvert.DeserializeObject<Login>(context.HttpContext.Session.GetString("usuarioSessao")).login);
                context.HttpContext.Session.SetString("usuarioSessao", JsonConvert.SerializeObject(novoLogin));
                return JsonConvert.SerializeObject(new { valid = true, message = "Renovação feita com sucesso" });
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { valid = false, message = e.Message });
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método requisitado por função AJAX (ou qualquer outro serviço de gerenciamento de requisições HTTP) para cadastrar um usuário no sistema
        public string insereUsuario([FromBody] LoginRegistroRequest request) =>
            JsonConvert.SerializeObject(login.registraUsuario(request));

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Método para remover um usuário do banco
        [FiltroUsuario]
        public IActionResult removerUsuario([FromQuery] string usuario)
        {
            login.removerUsuario(usuario);
            return RedirectToAction("Index", "Home");
        }
            
    }
} 