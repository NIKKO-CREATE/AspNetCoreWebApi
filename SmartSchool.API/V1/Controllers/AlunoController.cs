using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Helper;
using SmartSchool.API.Models;
using SmartSchool.API.V1.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);
            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
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
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _repo.GetAlunosByIdAsync(id, false);

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
        public async Task<IActionResult> Put(int id, AlunoRegistrarDto model)
        {
            var aluno = await _repo.GetAlunosByIdAsync(id);

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
        public async Task<IActionResult> Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = await _repo.GetAlunosByIdAsync(id);

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
        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _repo.GetAlunosByIdAsync(id);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);

            if (_repo.SaveChanges())
                return Ok($"Aluno {aluno.Nome} foi deletado");

            return Ok();
        }
    }
}
