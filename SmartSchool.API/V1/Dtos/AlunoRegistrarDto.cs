using System;

namespace SmartSchool.API.V1.Dtos
{
    public class AlunoRegistrarDto
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFinal { get; set; } = null; 
        public bool Ativo { get; set; } = true;
    }
}
