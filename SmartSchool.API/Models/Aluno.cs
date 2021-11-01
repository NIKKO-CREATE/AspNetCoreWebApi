using System;
using System.Collections.Generic;

namespace SmartSchool.API.Models
{
    public class Aluno
    {
        public Aluno(){}

        public Aluno(int id, string nome, string sobrenome, string telefone)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
        }

        public int Id { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String Telefone { get; set; }
        public IEnumerable<AlunoDisciplina> AlunosDisiplinas { get; set; }
    }
}
