using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.Models
{
    public class Vendedor
    {
        public int VendedorId { get; set; }
        public string Nome { get; set; }
        public long Cpf { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; }
        public virtual List<Venda> Vendas { get; set; }
    }
}