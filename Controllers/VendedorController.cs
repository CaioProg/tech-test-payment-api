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
    public class VendedorController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public VendedorController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{ObterVendedorPorId}")]
        public IActionResult ObterVendedorPorId(int ObterVendedorPorId)
        {
            var vendedor = _context.Vendedors.Find(ObterVendedorPorId);

            if(vendedor != null)
                return Ok(vendedor);

            return NotFound();
        }
        
        [HttpGet("ObterTodasOsVendedores")]
        public IActionResult ObterTodasOsVendedores()
        {
            var vendedores = _context.Vendedors;
            return Ok(vendedores);
        }

        [HttpPost]
        public IActionResult CriarVendedor(Vendedor vendedor)
        {
            if(vendedor.Cpf == 0)
                return BadRequest(new { Erro = "O CPF da Vendedor é obrigatório!" });

            _context.Add(vendedor);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterVendedorPorId), new { ObterVendedorPorId = vendedor.VendedorId }, vendedor);    
        }   

        [HttpPut("{id}")]
        public IActionResult AtualizarVendedor(int id, Vendedor vendedor)
        {   
            var vendedorBanco = _context.Vendedors.Find(id);

            if(vendedorBanco == null)
                return NotFound();

            vendedorBanco.Nome = vendedor.Nome;

            _context.Vendedors.Update(vendedorBanco);
            _context.SaveChanges();

            return Ok(vendedorBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarVendedor(int id)
        {
            var vendedor = _context.Vendedors.Find(id);

            if(vendedor == null)
                return NotFound();

            _context.Vendedors.Remove(vendedor);
            _context.SaveChanges();

            return NoContent();
        }
    }
}