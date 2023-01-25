using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public VendaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{ObterVendaPorId}")]
        public IActionResult ObterVendaPorId(int ObterVendaPorId) 
        {
            var venda = _context.Vendas.Find(ObterVendaPorId);

            if(venda != null)
                return Ok(venda);

            return NotFound();
        }

        [HttpPost]
        public IActionResult CriarVenda(Venda venda)
        {
            if(venda.Status.ToString() != "Aguardando pagamento")
                return BadRequest("Na criação da venda só é permitido o Status: Aguardando Pagamento");
            
            _context.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterVendaPorId), new { ObterVendaPorId = venda.VendaId }, venda);    
        }
    }
}