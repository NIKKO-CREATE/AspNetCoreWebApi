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
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository rep,IMapper mapper) 
        {
            _repo = rep;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var prof = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(prof));
        }

        [HttpGet("GetRegsitrar")]
        public IActionResult GetRegsitrar()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _repo.GetProfessoresById(id, false);

            if (prof == null)
                return BadRequest("Professor não encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(prof);

            return Ok(professorDto);
        }

        [HttpPost()]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var prof = _mapper.Map<Professor>(model);

            _repo.Add(prof);

            if (_repo.SaveChanges())
                return Created($"/api/Professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var prof = _repo.GetProfessoresById(id, false);

            if (prof == null)
                return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);

            _repo.Update(prof);

            if (_repo.SaveChanges())
                return Created($"/api/Professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var prof = _repo.GetProfessoresById(id);

            if (prof == null)
                return BadRequest("Professor não encontrado");
            
            _mapper.Map(model, prof);
            
            _repo.Update(prof);

            if (_repo.SaveChanges())
                return Created($"/api/Professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));

            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessoresById(id);

            if (professor == null)
                return BadRequest("Aluno não encontrado");

            _repo.Delete(professor);

            if (_repo.SaveChanges())
                return Ok($"Professor {professor.Nome} foi Deletado");

            return Ok("Professor nõa deletado");
        }
    }
}
