namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;
    using System;

    public class CartaException : Exception
    {
        public CartaException(Carta cartaJogada, string mensagem) 
            : base(mensagem)
        {
        }
    }
}