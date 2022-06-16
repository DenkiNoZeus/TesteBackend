using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteBackend.Models;

namespace TesteBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TesteBackendController : ControllerBase
    {
        private readonly DBContext context;

        public TesteBackendController(DBContext _context)
        {
            context = _context;
        }

        //GET TesteBackend/Equipes
        [HttpGet("Equipes")]
        public async Task<ActionResult<IEnumerable<Equipes>>> GetEquipes()
        {
            if(context == null)
            {
                return NotFound();
            }
            return await context.Equipes.ToListAsync();
        }

        //GET TesteBackend/Equipes/id
        [HttpGet("Equipes/{id}")]
        public async Task<ActionResult<IEnumerable<Equipes>>> GetEquipes(long id)
        {
            if(context == null)
            {
                return NotFound();
            }
            //var Equipe = await context.Equipes.FindAsync(id);
            var Equipe = await context.Equipes.Where(equipe => equipe.Id == id).ToListAsync();
            return Equipe;
        }

        //GET TesteBackend/Funcionarios
        [HttpGet("Funcionarios")]
        public async Task<ActionResult<IEnumerable<Funcionarios>>> GetFuncionarios()
        {
            if(context == null)
            {
                return NotFound();
            }
            return await context.Funcionarios.Include(funcionario => funcionario.Equipe).ToListAsync();
        }

        //GET TesteBackend/Funcionarios/id
        [HttpGet("Funcionarios/{id}")]
        public async Task<ActionResult<IEnumerable<Funcionarios>>> GetFuncionarios(long id)
        {
            if(context == null)
            {
                return NotFound();
            }
            var Funcionario = await context.Funcionarios.Where(funcionario => funcionario.Id == id).Include(funcionario => funcionario.Equipe).ToListAsync();
            return Funcionario;
        }

        //POST TesteBackend/Equipes
        [HttpPost("Equipes")]
        public async Task<ActionResult<Equipes>> PostEquipes(Equipes equipe)
        {
            context.Equipes.Add(equipe);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipes), new { id = equipe.Id }, equipe);
        }

        //POST TesteBackend/Funcionarios
        [HttpPost("Funcionarios")]
        public async Task<ActionResult<Funcionarios>> PostFuncionarios(FuncionariosDTO funcionario)
        {
            var equipe = await context.Equipes.FindAsync(funcionario.EquipeId);
            if(equipe == null)
            {
                return BadRequest();
            }
            if(funcionario.Cargo == "Gerente" && string.IsNullOrEmpty(funcionario.Email))
            {
                return BadRequest();
            }

            var entity = new Funcionarios();
            entity.Cargo = funcionario.Cargo;
            entity.Email = funcionario.Email;
            entity.Equipe = equipe;

            context.Funcionarios.Add(entity);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFuncionarios), new { id = entity.Id }, entity);
        }
    }
}