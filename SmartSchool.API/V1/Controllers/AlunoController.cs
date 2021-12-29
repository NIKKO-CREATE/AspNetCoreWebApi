using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.V1.Dtos;
using SmartSchool.API.Models;
using System.Collections.Generic;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// 
    /// </summary> 
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// This method return all students.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        /// <summary>
        /// This method return only one students.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRegsitrar")]
        public IActionResult GetRegsitrar()
        {
            return Ok (new AlunoRegistrarDto());
        }

        /// <summary>
        /// This method return only one studentes by "ID".
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunosById(id, false);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        /// <summary>
        /// This method add students.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);

            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não encontrado");
        }

        /// <summary>
        /// This method updates the student record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method partially updates the student record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method delete one studentes by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
