namespace TesteBackend.Models
{
    public class Funcionarios
    {
        public Funcionarios() { }
        public long Id { get; set; }
        public string Cargo { get; set; }
        public string? Email { get; set; }
        public Equipes Equipe { get; set; }
    }
}