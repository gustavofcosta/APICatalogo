using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger _logger;

    public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger)
    {
        _uof = uof;
        _logger = logger;
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
    {
        _logger.LogInformation("======== ============= Get api/categorias/produtos ======= ======================");
        //return _context.Categorias.Include(p=> p.Produtos).AsNoTracking().ToList();  não fazer desta forma, sempre incluir filtro
        //return await _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToListAsync();
        return null;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();
        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound("Categoria não encontrado");
        }

        var categoriaDto = new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemURL = categoria.ImagemURL,
        };
        return Ok(categoriaDto); 
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
            return BadRequest("Dados inválidos");

        var categoria = new Categoria()
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemURL = categoriaDto.ImagemURL,
        };
        _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();

        var novaCategoriaDto = new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemURL = categoria.ImagemURL,
        };

        return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não localizado...");
        }

        _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        return Ok(categoria);
    }
}
