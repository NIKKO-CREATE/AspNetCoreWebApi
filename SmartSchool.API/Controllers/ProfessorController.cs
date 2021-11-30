using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        public readonly SmartContext _context;
        public readonly IRepository _repo;

        public ProfessorController(SmartContext context, IRepository repo) 
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (prof == null)
                return BadRequest("Professor não encontrado");

            return Ok(prof);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome)
        {
            var prof = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if (prof == null)
                return BadRequest("Professor não encontrado");

            return Ok(prof);
        }

        [HttpPost()]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Profesor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
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
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id); 
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
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null)
                return BadRequest("Professor não encontrado");

            _repo.delete(professor);
            if (_repo.SaveChanges())
                return Ok("Professor deletado");

            return BadRequest("Professor não excluido");
        }
    }
}
