using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Cliente")]
    public class Cliente : BaseModelo
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

    }
}