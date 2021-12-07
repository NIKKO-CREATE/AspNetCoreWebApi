using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        public AlunoController(IRepository repo) 
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var resut = _repo.GetAllAlunos(true);
            return Ok(resut);
        }

        // Api/aluno/id
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunosById(id, false);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost()]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);

            if (_repo.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunosById(id);

            if (alu == null)
                return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if (_repo.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunosById(id);

            if (alu == null)
                return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if (_repo.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunosById(id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _repo.delete(aluno);
            if (_repo.SaveChanges())
                return Ok(aluno);

            return Ok();
        }
    }
}
