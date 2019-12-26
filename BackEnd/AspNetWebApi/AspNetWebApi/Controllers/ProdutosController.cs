using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Motivo do Try Catch: Evitar de perder tempo com alguma restrição declarada por engano na Model durante os testes

namespace AspNetWebApi.Controllers
{
    public class ProdutosController : ApiController
    {
        public class Produto
        {
            public long Id { get; set; }
            public string Descricao { get; set; }
            public float Valor { get; set; }
        }

        [HttpGet]
        public List<Produto> Get()
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var produtosModelo = contexto.Produtos.ToList();
                    var produtosProxy = new List<Produto>();

                    foreach (var produtoModelo in produtosModelo)
                    {
                        var contatoProxy = new Produto()
                        {
                            Id = produtoModelo.Id,
                            Descricao = produtoModelo.Descricao,
                            Valor = produtoModelo.Valor
                        };

                        produtosProxy.Add(contatoProxy);
                    }

                    return produtosProxy;
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
            }
        }

        [HttpGet]
        public Produto Get(long id)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var produtoModelo = contexto.Produtos
                    .Where(x => x.Id == id)
                    .Single();


                    var produtoProxy = new Produto()
                    {
                        Descricao = produtoModelo.Descricao,
                        Valor = produtoModelo.Valor
                    };

                    return produtoProxy;
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
            }
        }


        public class NovoProduto
        {
            public string Descricao { get; set; }
            public float Valor { get; set; }
        }

        [HttpPost]
        public void Post(NovoProduto novoProduto)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var mensagemModelo = new Models.Produto()
                    {
                        Descricao = novoProduto.Descricao,
                        Valor = novoProduto.Valor
                    };

                    contexto.Produtos.Add(mensagemModelo);
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
            }
        }
    }
}
