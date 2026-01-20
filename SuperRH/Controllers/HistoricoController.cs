using Microsoft.AspNetCore.Mvc;
using SuperRH.Data;
using SuperRH.Models;
using Microsoft.EntityFrameworkCore;

namespace SuperRH.Controllers
{
    public class HistoricoController : Controller
    {
        private readonly AppDbContext _context;

        public HistoricoController(AppDbContext context)
        {
            _context = context;
        }

        // Listagem de funcionários para escolher quem recebe o histórico
        public IActionResult Index()
        {
            var funcionarios = _context.Funcionarios
                .OrderBy(f => f.NomeCompleto)
                .ToList();
            return View(funcionarios);
        }

        // GET: Carrega o formulário no modal
        [HttpGet]
        public IActionResult Create(int idFuncionario)
        {
            var funcionario = _context.Funcionarios.Find(idFuncionario);
            if (funcionario == null) return NotFound();

            var model = new Historico
            {
                idFuncionario = idFuncionario,
                DataEvento = DateTime.Now
            };

            return PartialView("_CreatePartial", model);
        }

        // POST: Salva a ocorrência
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Historico historico)
        {
            if (ModelState.IsValid)
            {
                historico.DataRegistro = DateTime.Now;
                _context.Historico.Add(historico);
                _context.SaveChanges();
                return Ok();
            }
            return PartialView("_CreatePartial", historico);
        }

        public IActionResult Timeline(int id)
        {
            // Busca os históricos do funcionário específico, ordenando pelos mais recentes
            var listaHistorico = _context.Historico
                .Where(h => h.idFuncionario == id)
                .OrderByDescending(h => h.DataEvento)
                .ToList();

            // Busca o nome do funcionário para exibir no topo (opcional)
            ViewBag.NomeFuncionario = _context.Funcionarios
                .Where(f => f.idFuncionario == id)
                .Select(f => f.NomeCompleto)
                .FirstOrDefault();

            return PartialView("_TimelinePartial", listaHistorico);
        }
    }
}