namespace SuperRH.Models
{
    public class DashboardViewModel
    {
        public required string NomeUsuario { get; set; }
        public required string NivelAcesso { get; set; }

        // Colaboradores
        public int TotalColaboradores { get; set; }

        // Mockados (por enquanto)
        public int PresentesHoje { get; set; } = 118;
        public int FeriasProgramadas { get; set; } = 8;
        public int AlertasPonto { get; set; } = 3;
    }
}
