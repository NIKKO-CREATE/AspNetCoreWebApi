using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;
using System.Collections.Generic;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("GetRegsitrar")]
        public IActionResult GetRegsitrar()
        {
            return Ok (new AlunoRegistrarDto());
        }

        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunosById(id, false);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        [HttpPost()]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);

            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunosById(id);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunosById(id);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunosById(id);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);

            if (_repo.SaveChanges())
                return Ok($"Aluno {aluno.Nome} foi deletado");

            return Ok();
        }
    }
}
