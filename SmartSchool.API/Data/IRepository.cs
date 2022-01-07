using SmartSchool.API.Helper;
using SmartSchool.API.Models;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //Alunos 
        Task<PageLIst<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
        Aluno[] GetAllAlunos(bool includeProfessor = false);
        Task <Aluno[]> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Task <Aluno> GetAlunosByIdAsync(int alunoId, bool includeProfessor = false);
        Aluno GetAlunosById(int alunoId, bool includeProfessor = false);

        //Professores
        Professor[] GetAllProfessores(bool includeAluno = false);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
        Professor GetProfessoresById(int id, bool includeAlunos = false);
    }
}
