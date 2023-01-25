using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.Models
{
    public class Venda
    {   
        public int VendaId { get; set; }
        public string ItensVendidos { get; set; }
        public DateTime Data { get; set; }
        public int ProdutoID { get; set; }
        public int ClienteID { get; set; }
        public int VendedorId { get; set; }
        public string Status { get; set; }
    }
}