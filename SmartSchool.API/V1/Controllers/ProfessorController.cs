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
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// This method return all teachers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        /// <summary>
        /// This method return only one teacher.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRegsitrar")]
        public IActionResult GetRegsitrar()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        /// <summary>
        /// This method return only one teacher by "ID".
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessoresById(id, false);

            if (professor == null)
                return BadRequest("Professor não encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        /// <summary>
        /// This method add teacher.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);

            if (_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("Professor não encontrado");
        }

        /// <summary>
        /// This method updates the teacher record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessoresById(id);

            if (professor == null)
                return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("professor não atualizado");
        }

        /// <summary>
        /// This method partially updates the teacher record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessoresById(id);

            if (professor == null)
                return BadRequest("professor não encontrado");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<AlunoDto>(professor));

            return BadRequest("Professor não atualizado");
        }

        /// <summary>
        /// This method delete one teacher by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessoresById(id);

            if (professor == null)
                return BadRequest("professor não encontrado");

            _repo.Delete(professor);

            if (_repo.SaveChanges())
                return Ok($"Professor {professor.Nome} foi deletado");

            return Ok();
        }
    }
}
