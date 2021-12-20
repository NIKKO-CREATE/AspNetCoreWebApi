using System;

namespace SmartSchool.API.Dtos
{
    // DTO (Data transfer object)
    // Serve para transferir os dados do BD
    public class AlunoDto
    {

        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public bool Ativo { get; set; } = true; 
    }
}
