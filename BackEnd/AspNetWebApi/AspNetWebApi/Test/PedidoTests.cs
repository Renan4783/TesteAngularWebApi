using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;
using AspNetWebApi.Controllers;

namespace AspNetWebApi.Test
{
    public class PedidoTests
    {

        public static PedidosController TestController = new PedidosController();
        public static ClientesController TestControllerCliente = new ClientesController();
        public static ProdutosController TestControllerProduto = new ProdutosController();

        public static void Main()
        {
            //teste de inserção de dados e retorno via JSON
            PedidosController.NovoPedido pedido1 = new PedidosController.NovoPedido();
            List<ProdutosController.Produto> Produtos = new List<ProdutosController.Produto>();
            //Inserção do pedido
            pedido1.Numero = 1;
            pedido1.Valor = 100;
            pedido1.Desconto = 0;
            pedido1.IdCliente = TestControllerCliente.Get(1).Id;
            pedido1.ValorTotal = 100;
            //Inserção dos produtos
            Produtos.Add(TestControllerProduto.Get(1));
            Produtos.Add(TestControllerProduto.Get(2));
            pedido1.Produtos = Produtos.Select(item => new Produto()
            {
                Id = item.Id,
                Descricao = item.Descricao,
                Valor = item.Valor
            }
            ).ToList();
            TestController.Post(pedido1);
        }
    }
}