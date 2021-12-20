using System;

namespace SmartSchool.API.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFinal { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}
