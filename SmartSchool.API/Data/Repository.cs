using SmartSchool.API.Models;
using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;


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
        public Aluno[] GetAllAlunos(bool includeDisciplina)
        {
            IQueryable<Aluno> query = _context.Alunos;

            query = query.AsNoTracking().OrderBy(a => a.Id);
            
            if (includeDisciplina)
            {
                query = query.Include(a => a.AlunosDisiplinas)
                             .ThenInclude(ad => ad.Diciplina)
                             .ThenInclude(d => d.Professor);
            }
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId()
        {
            throw new NotImplementedException();
        }

        public Aluno GetAlunosById()
        {
            throw new NotImplementedException();
        }

        // Professor 
        public Professor[] GetAllProfessores()
        {
            throw new NotImplementedException();
        }

        public Professor[] GetAllProfessoresByDisciplinaId()
        {
            throw new NotImplementedException();
        }

        public Professor GetAllProfessoresById()
        {
            throw new NotImplementedException();
        }
    }
}
