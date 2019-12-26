using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AspNetWebApi.Context;
using AspNetWebApi.Models;

// Motivo do Try Catch: Evitar de perder tempo com alguma restrição declarada por engano na Model durante os testes

namespace AspNetWebApi.Controllers
{
    public class PedidosController : ApiController
    {

        public class Pedido
        {
            public long Id { get; set; }
            public int Numero { get; set; }
            public DateTime Data { get; set; }
            public Cliente Cliente { get; set; }
            public float Valor { get; set; }
            public float Desconto { get; set; }
            public float ValorTotal { get; set; }
        }

        [HttpGet]
        public List<Pedido> Get()
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var pedidosModelo = contexto.Pedidos.ToList();
                    var pedidosProxy = new List<Pedido>();

                    foreach (var pedidoModelo in pedidosModelo)
                    {
                       
                        var contatoProxy = new Pedido()
                        {
                            Id = pedidoModelo.Id,
                            Numero = pedidoModelo.Numero,
                            Data = pedidoModelo.Data,
                            Cliente = pedidoModelo.Cliente,
                            Valor = pedidoModelo.Valor,
                            Desconto = pedidoModelo.Desconto,
                            ValorTotal = pedidoModelo.ValorTotal
                        };
                        pedidosProxy.Add(contatoProxy);
                    }
  
                    return pedidosProxy;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx) //Código para capturar exceções do banco
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                catch (Exception dbEx)
                {
                    throw dbEx;
                }
            }
        }


        [HttpGet]
        public Pedido Get(long id)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var pedidoModelo = contexto.Pedidos
                    .Where(x => x.Id == id)
                    .Single();

                    var pedidoProxy = new Pedido()
                    {
                        Id = pedidoModelo.Id,
                        Numero = pedidoModelo.Numero,
                        Data = pedidoModelo.Data,
                        Cliente = pedidoModelo.Cliente,
                        Valor = pedidoModelo.Valor,
                        Desconto = pedidoModelo.Desconto,
                        ValorTotal = pedidoModelo.ValorTotal
                    };

                    return pedidoProxy;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx) //Código para capturar exceções do banco
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                catch (Exception dbEx)
                {
                    throw dbEx;
                }
            }
        }

        public class NovoPedido
        {
            public int Numero { get; set; }
            public DateTime Data { get; set; }
            public long IdCliente { get; set; }
            public float Valor { get; set; }
            public float Desconto { get; set; }
            public float ValorTotal { get; set; }
            public List<Produto> Produtos { get; set; }
        }

        [HttpPost]
        public void Post(NovoPedido novoPedido)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var clienteModelo = contexto.Clientes
                    .Where(x => x.Id == novoPedido.IdCliente)
                    .Single();

                    var pedidoModelo = new Models.Pedido()
                    {
                        Numero = novoPedido.Numero,
                        Data = DateTime.Now,
                        Valor = novoPedido.Valor,
                        Cliente = clienteModelo,
                        Desconto = novoPedido.Desconto,
                        ValorTotal = novoPedido.ValorTotal,
                        Produtos = novoPedido.Produtos
                    };
                    contexto.Pedidos.Add(pedidoModelo);
                    contexto.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx) //Código para capturar exceções do banco
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                catch (Exception dbEx)
                {
                    throw dbEx;
                }
            }
        }

        [HttpGet]
        [Route("api/pedidos/{id}/produtos")]
        public List<Produto> Produtos(long id)
        {
            using (var contexto = new Contexto())
            {
                var produtosProxy = new List<Produto>();

                var pedidoModelo = contexto.Pedidos
                    .Include(x => x.Produtos)
                    .Where(x => x.Id == id)
                    .Single();

                foreach (var produtoModelos in pedidoModelo.Produtos)
                {
                    var produtoProxy = new Produto()
                    {
                        Valor = produtoModelos.Valor,
                        Descricao = produtoModelos.Descricao
                    };

                    produtosProxy.Add(produtoProxy);
                }

                return produtosProxy;
            }
        }
    }
}