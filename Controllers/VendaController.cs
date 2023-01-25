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
            if(venda.Status != "Aguardando pagamento")
                return BadRequest("Na criação da venda só é permitido o Status: Aguardando Pagamento");
            
            _context.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterVendaPorId), new { ObterVendaPorId = venda.VendaId }, venda);    
        }

        [HttpPatch]
        public IActionResult EditarVenda(Venda venda, int id)
        {
            var vendaBanco = _context.Vendas.Find(id);
            string verificacaoStatus = venda.Status;

            switch (verificacaoStatus)
            {
                case "Pagamento aprovado":
                    if(vendaBanco.Status == "Aguardando pagamento")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }
                        return BadRequest($"Não é possível editar a venda pra esse Status! {vendaBanco.Status}");

                case "Cancelada":
                    if(vendaBanco.Status == "Aguardando pagamento" || vendaBanco.Status == "Pagamento aprovado")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }
                        return BadRequest($"Não é possível editar a venda pra esse Status! {vendaBanco.Status}");

                case "Enviado para Transportadora":
                    if(vendaBanco.Status == "Pagamento aprovado")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }
                        return BadRequest($"Não é possível editar a venda pra esse Status! {vendaBanco.Status}");

                case "Entregue":
                    if(vendaBanco.Status == "Enviado para Transportadora")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }                        
                        return BadRequest($"Não é possível editar a venda pra esse Status! {vendaBanco.Status}");

                default:
                    return BadRequest($"Escolha um Status válido!");
                    break;
            }

            vendaBanco.ItensVendidos = venda.ItensVendidos;
            vendaBanco.Data = venda.Data;
            vendaBanco.ClienteID = venda.ClienteID;
            vendaBanco.VendedorId = venda.VendedorId;

            _context.Vendas.Update(vendaBanco);
            _context.SaveChanges();

            return Ok(vendaBanco);
        }
    }
}