using Microsoft.AspNetCore.Mvc;
using SuperRH.Data;
using SuperRH.Models;
using System.Linq;

namespace SuperRH.Controllers
{
    // Recomendo usar no plural para bater com o seu JavaScript da Index
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        // Listagem Principal
        public IActionResult Index()
        {
            var lista = _context.Funcionarios.OrderByDescending(f => f.idFuncionario).ToList();
            return View(lista);
        }

        // GET: Create (Retorna a Partial para o Modal)
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreatePartial");
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                funcionario.DataCadastro = DateTime.Now;
                funcionario.Status = true;
                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();
                return Ok(); // Retorna 200 OK para o AJAX fechar o modal
            }
            return PartialView("_CreatePartial", funcionario);
        }

        // GET: Edit (Retorna a Partial para o Modal)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null) return NotFound();

            return PartialView("_EditPartial", funcionario);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                var funcDb = _context.Funcionarios.Find(funcionario.idFuncionario);
                if (funcDb == null) return NotFound();

                // Atualizando os dados pessoais
                funcDb.NomeCompleto = funcionario.NomeCompleto;
                funcDb.CPF = funcionario.CPF;
                funcDb.RG = funcionario.RG;
                funcDb.OrgaoEmissor = funcionario.OrgaoEmissor;
                funcDb.DataNascimento = funcionario.DataNascimento;
                funcDb.Sexo = funcionario.Sexo;
                funcDb.EstadoCivil = funcionario.EstadoCivil;
                funcDb.Naturalidade = funcionario.Naturalidade;
                funcDb.NomeMae = funcionario.NomeMae;
                funcDb.NomePai = funcionario.NomePai;

                _context.Funcionarios.Update(funcDb);
                _context.SaveChanges();
                return Ok();
            }
            return PartialView("_EditPartial", funcionario);
        }

        // GET: Details (Retorna a Partial para o Modal)
        public IActionResult Details(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null) return NotFound();

            return PartialView("_DetailsPartial", funcionario);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}