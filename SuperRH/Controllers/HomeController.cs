using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SuperRH.Models;

namespace SuperRH.Controllers
{
    public class HomeController : Controller
    {

        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' não foi encontrada."
                );
        }


        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                NomeUsuario = "Administrador",
                NivelAcesso = "Admin"
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // TOTAL DE COLABORADORES ATIVOS
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Colaboradores WHERE Status = 1", conn))
                {
                    model.TotalColaboradores = (int)cmd.ExecuteScalar();
                }

                // USUÁRIO (SE EXISTIR NO BANCO)
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1 NomeCompleto, NivelAcesso
            FROM Usuarios
            WHERE Status = 1
            ORDER BY DataCriacao DESC
        ", conn))
                {
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        model.NomeUsuario = reader["NomeCompleto"].ToString();
                        model.NivelAcesso = reader["NivelAcesso"].ToString();
                    }
                }
            }

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
