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

        [HttpPost]
        public IActionResult CriarCategoria(Categoria categoria)
        {
            if(categoria.Nome == null)
                return BadRequest(new { Erro = "O nome da Categoria é obrigatório!" });

            _context.Add(categoria);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterCategoriaPorId), new { categoriaId = categoria.CategoriaID }, categoria);    
        }   

    }
}