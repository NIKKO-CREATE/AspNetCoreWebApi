using System;
using System.Collections.Generic;

namespace SmartSchool.API.Models
{
    public class Professor
    {
        public Professor() {}

        public Professor(int id, int registro, string nome, string sobrenome)
        {
            this.Id = id;
            this.Registro = registro;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
        }

        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFinal { get; set; } = null; //"?" pode ser nulo
        public bool Ativo { get; set; } = true; // Sempre que não setarmos ele vai ser true
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
