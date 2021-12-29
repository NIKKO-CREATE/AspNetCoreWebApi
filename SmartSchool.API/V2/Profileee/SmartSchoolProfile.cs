using AutoMapper;
using SmartSchool.API.Helper;
using SmartSchool.API.Models;
using SmartSchool.API.V2.Dtos;

namespace SmartSchool.API.V2.Profileee
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
        }
    }
}
