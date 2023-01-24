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
    public class ClienteController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public ClienteController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{ObterClientePorId}")]
        public IActionResult ObterClientePorId(int ObterClientePorId)
        {
            var cliente = _context.Clientes.Find(ObterClientePorId);
            
            if(cliente == null)
                return NotFound();

            return Ok(cliente);    
        }

        [HttpPost]
        public IActionResult CriarCliente(Cliente cliente)
        {
            if(cliente.Endereco == "")
                return BadRequest(new { Erro = "O campo Endereço é obrigatório!" });
            
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterClientePorId), new { ObterClientePorId = cliente.ClienteID }, cliente);    
        }

        [HttpGet("ObterTodosOsClientes")]
        public IActionResult ObterTodosOsClientes()
        {
            var clientes = _context.Clientes;

            return Ok(clientes);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(int id, Cliente cliente)
        {
            var clienteBanco = _context.Clientes.Find(id);

            if(clienteBanco == null)
                return NotFound();

            clienteBanco.Endereco = cliente.Endereco;
            
            _context.Clientes.Update(clienteBanco);
            _context.SaveChanges();

            return Ok(clienteBanco);
        }

        [HttpDelete]
        public IActionResult DeletarCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if(cliente == null)
                return NotFound();
            
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return NoContent();
        }

    }
}