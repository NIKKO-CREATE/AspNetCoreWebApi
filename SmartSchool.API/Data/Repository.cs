using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;
using System;
using System.Linq;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _ = _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _ = _context.Update(entity);
        }

        public void delete<T>(T entity) where T : class
        {
            _ = _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            _ = _context.SaveChanges();
            return true;
        }

        // Aluno
        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos; // Atribui para "Query" o "Alunos" 

            if (includeProfessor) // Se for True, ele irá fazer todos esses "joins"
            {
                query = query.Include(a => a.AlunosDisiplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos; 

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisiplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.AlunosDisiplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunosById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos; 

            if (includeProfessor) 
            {
                query = query.Include(a => a.AlunosDisiplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId); // Irá recuperar o aluno 

            return query.FirstOrDefault();
        }

        // Professor 
        public Professor[] GetAllProfessores(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(p => p.AlunosDisiplinas)
                             .ThenInclude(id => id.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(p => p.AlunosDisiplinas)
                             .ThenInclude(id => id.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(p => p.Id)
                         .Where(aluno => aluno.Disciplinas.Any(
                           professor => professor.AlunosDisiplinas.Any(pId => pId.DisciplinaId == disciplinaId)));

            return query.ToArray();
        }

        public Professor GetProfessoresById(int id, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(p => p.AlunosDisiplinas)
                             .ThenInclude(id => id.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(p => p.Id)
                         .Where(p => p.Id == id);

            return query.FirstOrDefault();
        }
    }
}
