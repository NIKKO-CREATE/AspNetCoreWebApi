using AutoMapper;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Helper
{
    public class SmartSchoolProfile : Profile
    {
        // Mapeamento De -> Para
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                alunoDto => alunoDto.Nome,
                aluno => aluno.MapFrom(src => $"{src.Nome} {src.Sobrenome}")) // Aluno é mapeado pelo AlunoDto
                .ForMember(
                    alunoDto => alunoDto.Idade,
                    aluno => aluno.MapFrom(src => src.DataNascimento.GetCurrentAge())
                );

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            //Professor Mapeamento
            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                professorDto => professorDto.Nome,
                professor => professor.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));

            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();
        }
    }
}
