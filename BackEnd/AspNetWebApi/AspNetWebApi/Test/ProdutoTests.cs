using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;
using AspNetWebApi.Controllers;

namespace AspNetWebApi.Test
{
    public class ProdutoTests
    {

        public static ProdutosController TestController = new ProdutosController();

        public static void Main()
        {
            //teste de inserção de dados e retorno via JSON
            ProdutosController.NovoProduto produto1 = new ProdutosController.NovoProduto();
            produto1.Descricao = "bla bla";
            produto1.Valor = 100;
            TestController.Post(produto1);
        }
    }
}