using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperRH.Data;
using SuperRH.Models;

public class HistoricoController : Controller
{
    private readonly AppDbContext _context;

    public HistoricoController(AppDbContext context)
    {
        _context = context;
    }

    // LISTA COLABORADORES
    public IActionResult Index()
    {
        var colaboradores = _context.Colaboradores.ToList();
        return View(colaboradores);
    }

    // TIMELINE (PARTIAL)
    public IActionResult Timeline(int idColaborador)
    {
        var historico = _context.Historicos
            .Where(h => h.idColaborador == idColaborador)
            .OrderByDescending(h => h.DataEvento)
            .ToList();

        return PartialView("_TimelinePartial", historico);
    }

    // DETALHES (PARTIAL)
    public IActionResult Detalhes(int id)
    {
        var historico = _context.Historicos
            .Include(h => h.Colaborador)
            .FirstOrDefault(h => h.idHistorico == id);

        if (historico == null)
            return NotFound();

        return PartialView("_DetalhesHistoricoPartial", historico);
    }

    // CREATE (PARTIAL)
    public IActionResult Create(int idColaborador)
    {
        var model = new Historico
        {
            idColaborador = idColaborador,
            DataEvento = DateTime.Now
        };

        return PartialView("_CreatePartial", model);
    }

    [HttpPost]
    public IActionResult Create(Historico model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        model.DataRegistro = DateTime.Now;

        _context.Historicos.Add(model);
        _context.SaveChanges();

        return Json(new { sucesso = true });
    }

}
