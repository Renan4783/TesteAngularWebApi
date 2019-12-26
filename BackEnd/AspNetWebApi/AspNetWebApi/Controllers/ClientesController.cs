using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Motivo do Try Catch: Evitar de perder tempo com alguma restrição declarada por engano na Model durante os testes

namespace AspNetWebApi.Controllers
{
    public class ClientesController : ApiController
    {
        public class Cliente
        {
            public long Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
        }

        [HttpGet]
        public List<Cliente> Get()
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var clientesModelo = contexto.Clientes.ToList();
                    var clientesProxy = new List<Cliente>();

                    foreach (var clienteModelo in clientesModelo)
                    {
                        var clienteProxy = new Cliente()
                        {
                            Id = clienteModelo.Id,
                            Nome = clienteModelo.Nome,
                            Email = clienteModelo.Email
                        };

                        clientesProxy.Add(clienteProxy);
                    }

                    return clientesProxy;
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
        public Cliente Get(long id)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var clienteModelo = contexto.Clientes
                    .Where(x => x.Id == id)
                    .Single();

                    var clienteProxy = new Cliente()
                    {
                        Id = clienteModelo.Id,
                        Nome = clienteModelo.Nome,
                        Email = clienteModelo.Email
                    };

                    return clienteProxy;
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

        public class NovoCliente
        {
            public string Nome { get; set; }
            public string Email { get; set; }

        }

        [HttpPost]
        public void Post(NovoCliente novoCliente)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var contatoModelo = new Models.Cliente()
                    {
                        Nome = novoCliente.Nome,
                        Email = novoCliente.Email
                    };

                    contexto.Clientes.Add(contatoModelo);
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
    }
}
