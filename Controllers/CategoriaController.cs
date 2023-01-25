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
    public class CategoriaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public CategoriaController(OrganizadorContext context)
        {
            _context = context;
        }

        string messageError = "Esse Id não está vinculado a nenhuma Categoria.";

        [HttpGet("{ObterCategoriaPorId}")]
        public IActionResult ObterCategoriaPorId(int ObterCategoriaPorId)
        {
            var categoria = _context.Categorias.Find(ObterCategoriaPorId);

            if(categoria != null)
                return Ok(categoria);

            return NotFound(messageError);
        }
        
        [HttpGet("ObterTodasAsCategorias")]
        public IActionResult ObterTodasAsCategorias()
        {
            var categorias = _context.Categorias;
            return Ok(categorias);
        }

        [HttpPost]
        public IActionResult CriarCategoria(Categoria categoria)
        {
            if(categoria.Nome == null)
                return BadRequest(new { Erro = "O nome da Categoria é obrigatório!" });

            _context.Add(categoria);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterCategoriaPorId), new { ObterCategoriaPorId = categoria.CategoriaID }, categoria);    
        }   

        [HttpPut("{id}")]
        public IActionResult AtualizarCategoria(int id, Categoria categoria)
        {   
            var categoriaBanco = _context.Categorias.Find(id);

            if(categoriaBanco == null)
                return NotFound(messageError);

            categoriaBanco.Nome = categoria.Nome;

            _context.Categorias.Update(categoriaBanco);
            _context.SaveChanges();

            return Ok(categoriaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if(categoria == null)
                return NotFound(messageError);

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return NoContent();
        }
    }
}