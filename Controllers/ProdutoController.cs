using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public ProdutoController(OrganizadorContext context)
        {
            _context = context;
        }


        [HttpGet("{ObterProdutoPorId}")]
        public IActionResult ObterProdutoPorId(int ObterProdutoPorId)
        {
            var produto = _context.Produtos.Find(ObterProdutoPorId);
            
            if(produto == null)
            return NotFound();

            return Ok(produto);    
        }

        [HttpPost]
        public IActionResult CriarProduto(Produto produto)
        {
            if(produto.Nome == "" || produto.Nome == null || produto.Nome == " ")
                return BadRequest(new { Erro = "O campo Nome é obrigatório!" });
            
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterProdutoPorId), new { ObterProdutoPorId = produto.ProdutoID }, produto);    
        }

        [HttpGet("ObterTodosOsProdutos")]
        public IActionResult ObterTodosOsProdutos()
        {
            var produtos = _context.Produtos;

            return Ok(produtos);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarProduto(int id, Produto produto)
        {
            var produtoBanco = _context.Produtos.Find(id);

            if(id == null)
                return NotFound();

            produtoBanco.Nome = produto.Nome;
            produtoBanco.Categoria = produto.Categoria;
            produtoBanco.Preco = produto.Preco;
            
            _context.Produtos.Update(produtoBanco);
            _context.SaveChanges();

            return Ok(produtoBanco);
        }

        [HttpDelete]
        public IActionResult DeletarProduto(int id)
        {
            var produto = _context.Produtos.Find(id);

            if(id == null)
                return NotFound();
            
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return NoContent();
        }
    }
}