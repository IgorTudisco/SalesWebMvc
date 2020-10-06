using System;


namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        // Fazendo uma clase para tratar nossos erros nós dá mais liberdade e controle sobre o nosso programa

        public NotFoundException(string menssage) : base(menssage)
        {

        }


    }
}
