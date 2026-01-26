using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperRH.Data;
using SuperRH.Models;
using System;
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

        /* =========================
           LISTAGEM
        ========================== */
        public IActionResult Index()
        {
            var lista = _context.Usuarios
                .OrderByDescending(u => u.idUsuario)
                .ToList();

            return View(lista);
        }

        /* =========================
           CREATE - GET
        ========================== */
        [HttpGet]
        public IActionResult Create()
        {
            CarregarColaboradores();
            return PartialView("_CreatePartial", new Usuario());
        }

        /* =========================
           CREATE - POST
        ========================== */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // ❌ SEM HASH (temporário)
                // usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);

                usuario.DataCriacao = DateTime.Now;
                usuario.Status = true;

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Colaboradores = _context.Colaboradores
                .OrderBy(c => c.NomeCompleto)
                .Select(c => new SelectListItem
                {
                    Value = c.idColaborador.ToString(),
                    Text = c.NomeCompleto
                })
                .ToList();

            return PartialView("_CreatePartial", usuario);
        }


        /* =========================
           EDIT - GET
        ========================== */
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user == null) return NotFound();

            CarregarColaboradores();
            return PartialView("_EditPartial", user);
        }

        /* =========================
           EDIT - POST
        ========================== */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            ModelState.Remove("SenhaHash");

            if (ModelState.IsValid)
            {
                var userDb = _context.Usuarios.FirstOrDefault(u => u.idUsuario == usuario.idUsuario);
                if (userDb == null) return NotFound();

                userDb.NomeCompleto = usuario.NomeCompleto;
                userDb.Email = usuario.Email;
                userDb.Login = usuario.Login;
                userDb.NivelAcesso = usuario.NivelAcesso;
                userDb.idColaborador = usuario.idColaborador;

                _context.SaveChanges();
                return Ok();
            }

            CarregarColaboradores();
            return PartialView("_EditPartial", usuario);
        }

        /* =========================
           DETAILS
        ========================== */
        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _context.Usuarios
                .Where(u => u.idUsuario == id)
                .Select(u => new Usuario
                {
                    idUsuario = u.idUsuario,
                    NomeCompleto = u.NomeCompleto,
                    Login = u.Login,
                    Email = u.Email,
                    NivelAcesso = u.NivelAcesso,
                    Status = u.Status,
                    DataCriacao = u.DataCriacao,
                    Colaborador = u.Colaborador
                })
                .FirstOrDefault();

            if (user == null) return NotFound();

            return PartialView("_DetailsPartial", user);
        }

        /* =========================
           DELETE
        ========================== */
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

        /* =========================
           MÉTODO AUXILIAR
        ========================== */
        private void CarregarColaboradores()
        {
            ViewBag.Colaboradores = _context.Colaboradores
                .OrderBy(c => c.NomeCompleto)
                .Select(c => new SelectListItem
                {
                    Value = c.idColaborador.ToString(),
                    Text = c.NomeCompleto
                })
                .ToList();
        }
    }
}
