using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;
using AspNetWebApi.Controllers;

namespace AspNetWebApi.Test
{
    public class ClienteTests
    {

        public static ClientesController TestController = new ClientesController();

        public static void Main()
        {
            //teste de inserção de dados e retorno via JSON
            ClientesController.NovoCliente cliente1 = new ClientesController.NovoCliente();
            cliente1.Nome = "Joao";
            cliente1.Email = "test@test";
            TestController.Post(cliente1);
        }
    }
}