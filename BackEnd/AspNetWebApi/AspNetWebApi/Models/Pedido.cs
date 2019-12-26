using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Pedido")]
    public class Pedido : BaseModelo
    {
        [Required]
        public int Numero { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public virtual Cliente Cliente { get; set; }

        public List<Produto> Produtos { get; set; }

        [Required]
        public float Valor { get; set; }

        public float Desconto { get; set; }

        [Required]
        public float ValorTotal { get; set; }
    }
}