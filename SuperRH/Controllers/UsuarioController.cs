using Microsoft.AspNetCore.Mvc;
using SuperRH.Data;
using SuperRH.Models;
using System.Linq;

namespace SuperRH.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Usuarios.OrderByDescending(u => u.idUsuario).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create() => PartialView("_CreatePartial");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.DataCriacao = DateTime.Now;
                usuario.Status = true;
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreatePartial", usuario);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user == null) return NotFound();
            return PartialView("_EditPartial", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            ModelState.Remove("SenhaHash");

            if (ModelState.IsValid)
            {
                try
                {
                    var userDb = _context.Usuarios.FirstOrDefault(u => u.idUsuario == usuario.idUsuario);

                    if (userDb == null) return NotFound();

                    userDb.NomeCompleto = usuario.NomeCompleto;
                    userDb.Email = usuario.Email;
                    userDb.Login = usuario.Login;
                    userDb.NivelAcesso = usuario.NivelAcesso;

                    _context.Usuarios.Update(userDb);
                    _context.SaveChanges();

                    return Ok(); 
                }
                catch (Exception ex)
                {
                    return BadRequest("Erro ao persistir dados: " + ex.Message);
                }
            }

            return PartialView("_EditPartial", usuario);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user == null) return NotFound();
            return PartialView("_DetailsPartial", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}