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
        [HttpGet("ObterTodasAsVendas")]
        public IActionResult ObterTodasAsVendas()
        {
            var vendas = _context.Vendas;

            return Ok(vendas);

        }

        [HttpPost]
        public IActionResult CriarVenda(Venda venda)
        {
            if(venda.Status != "Aguardando pagamento")
                return BadRequest("Na criação da venda só é permitido o Status: Aguardando pagamento");
            
            _context.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterVendaPorId), new { ObterVendaPorId = venda.VendaId }, venda);    
        }

        [HttpPatch]
        public IActionResult EditarVenda(Venda venda, int id)
        {
            var vendaBanco = _context.Vendas.Find(id);
            string verificacaoStatus = venda.Status;

            string logicaStatus = "Não é possível editar a venda pra esse Status! Essas são as movimentações de Status aceitas: \n \n" + 
                                  "De: Aguardando pagamento Para: Pagamento Aprovado \n" +
                                  "De: Aguardando pagamento Para: Cancelada \n" +
                                  "De: Pagamento Aprovado Para: Enviado para Transportadora \n" +
                                  "De: Pagamento Aprovado Para: Cancelada \n" +
                                  "De: Enviado para Transportadora Para: Entregue \n";

            switch (verificacaoStatus)
            {
                case "Pagamento Aprovado":
                    if(vendaBanco.Status == "Aguardando pagamento")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }
                        return BadRequest(logicaStatus);

                case "Cancelada":
                    if(vendaBanco.Status == "Aguardando pagamento" || vendaBanco.Status == "Pagamento Aprovado")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }
                        return BadRequest(logicaStatus);

                case "Enviado para Transportadora":
                    if(vendaBanco.Status == "Pagamento Aprovado")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }
                        return BadRequest(logicaStatus);

                case "Entregue":
                    if(vendaBanco.Status == "Enviado para Transportadora")
                    {
                        vendaBanco.Status = venda.Status;
                        break;
                    }                        
                        return BadRequest(logicaStatus);

                default:
                    return BadRequest($"Escolha um Status válido! \nLista de Status: Pagamento Aprovado, Enviado para Transportadora, Entregue, Cancelada,");
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