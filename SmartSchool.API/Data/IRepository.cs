using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {

        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void delete<T>(T entity) where T : class;
        bool SaveChanges();

        //Alunos 
        Aluno[] GetAllAlunos();
        Aluno[] GetAllAlunosByDisciplinaId();
        Aluno GetAlunosById();

        //Professores
        Professor[] GetAllProfessores();
        Professor[] GetAllProfessoresByDisciplinaId();
        Professor GetAllProfessoresById();
    }
}
