using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        public ProfessorController(IRepository repo) 
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var prof = _repo.GetAllProfessores(true);
            return Ok(prof);
        }

        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _repo.GetProfessoresById(id, false);
            if (prof == null)
                return BadRequest("Professor não encontrado");

            return Ok(prof);
        }

        [HttpPost()]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);

            if (_repo.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetProfessoresById(id, false);
            if (prof == null)
                return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            if (_repo.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessoresById(id);
            if (prof == null)
                return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            if (_repo.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessoresById(id);
            if (professor == null)
                return BadRequest("Aluno não encontrado");

            _repo.delete(professor);
            if (_repo.SaveChanges())
                return Ok(professor + "Professor Deletado");

            return Ok("Professor nõa deletado");
        }
    }
}
