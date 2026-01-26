using Microsoft.AspNetCore.Mvc;
using SuperRH.Data;
using SuperRH.Models;
using System;
using System.Linq;

namespace SuperRH.Controllers
{
    // Controller de Colaboradores
    public class ColaboradorController : Controller
    {
        private readonly AppDbContext _context;

        public ColaboradorController(AppDbContext context)
        {
            _context = context;
        }

        // LISTAGEM PRINCIPAL
        public IActionResult Index()
        {
            var lista = _context.Colaboradores
                .OrderByDescending(c => c.idColaborador)
                .ToList();

            return View(lista);
        }

        // GET: Create (Partial View - Modal)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cargos = _context.Cargos
                .Where(c => c.Status == true)
                .OrderBy(c => c.NomeCargo)
                .ToList();

            return PartialView("_CreatePartial");
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Colaborador colaborador)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cargos = _context.Cargos.ToList();
                return PartialView("_CreatePartial", colaborador);
            }

            colaborador.DataCadastro = DateTime.Now;
            colaborador.Status = true;

            _context.Colaboradores.Add(colaborador);
            _context.SaveChanges();

            return Ok();
        }


        // GET: Edit (Partial View - Modal)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var colaborador = _context.Colaboradores.Find(id);
            if (colaborador == null)
                return NotFound();

            return PartialView("_EditPartial", colaborador);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                var colabDb = _context.Colaboradores
                    .Find(colaborador.idColaborador);

                if (colabDb == null)
                    return NotFound();

                // Atualização dos dados pessoais
                colabDb.NomeCompleto = colaborador.NomeCompleto;
                colabDb.CPF = colaborador.CPF;
                colabDb.RG = colaborador.RG;
                colabDb.OrgaoEmissor = colaborador.OrgaoEmissor;
                colabDb.DataNascimento = colaborador.DataNascimento;
                colabDb.Sexo = colaborador.Sexo;
                colabDb.EstadoCivil = colaborador.EstadoCivil;
                colabDb.Naturalidade = colaborador.Naturalidade;
                colabDb.NomeMae = colaborador.NomeMae;
                colabDb.NomePai = colaborador.NomePai;
                colabDb.idCargo = colaborador.idCargo;

                _context.Colaboradores.Update(colabDb);
                _context.SaveChanges();

                return Ok();
            }

            return PartialView("_EditPartial", colaborador);
        }

        // GET: Details (Partial View - Modal)
        [HttpGet]
        public IActionResult Details(int id)
        {
            var colaborador = _context.Colaboradores.Find(id);
            if (colaborador == null)
                return NotFound();

            return PartialView("_DetailsPartial", colaborador);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var colaborador = _context.Colaboradores.Find(id);
            if (colaborador != null)
            {
                _context.Colaboradores.Remove(colaborador);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
