using Microsoft.AspNetCore.Mvc;
using SuperRH.Data;
using SuperRH.Models;
using System.Linq;

namespace SuperRH.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        // Tela de login
        public IActionResult Index()
        {
            // REMOVIDO: Verificação de sessão (se já está logado)
            // Como não estamos usando sessão agora, sempre mostra a tela de login.
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { sucesso = false, mensagem = "Dados inválidos." });

            // Procura usuário pelo login, senha e status ativo
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Login == model.Usuario
                                  && u.SenhaHash == model.Senha
                                  && u.Status);

            if (usuario == null)
                return Json(new { sucesso = false, mensagem = "Usuário ou senha incorretos." });

            // REMOVIDO: Gravação na Sessão (HttpContext.Session)
            // Aqui é onde você guardaria o ID do usuário futuramente.
            // Por enquanto, apenas confirmamos que a senha bateu.

            // RETORNA SUCESSO E A URL DA HOME
            return Json(new
            {
                sucesso = true,
                url = Url.Action("Index", "Home") // O JavaScript vai ler isso e redirecionar
            });
        }

        public IActionResult Logout()
        {
            // REMOVIDO: Limpeza da sessão
            return RedirectToAction("Index", "Login");
        }
    }
}