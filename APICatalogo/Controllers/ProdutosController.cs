using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{   
    private readonly IUnitOfWork _uof;

    public ProdutosController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult <IEnumerable<Produto>> GetProdutosCategorias(int id)
    {
        var produto = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

        if(produto is null) 
            return NotFound();

        return Ok(produto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
       
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name="ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null) 
        {
            return NotFound("Produto não encontrado");
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto) 
    {
        if (produto is null)
            return BadRequest();

        _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não localizado...");
        }

        _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        return Ok(produto);
    }
}
