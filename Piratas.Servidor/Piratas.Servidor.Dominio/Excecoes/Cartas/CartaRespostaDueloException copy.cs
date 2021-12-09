namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;
    using System;

    public class CartaRespostaDueloException : CartaException
    {
        public CartaRespostaDueloException(Carta cartaJogada) 
            : base(cartaJogada, $"Carta \"{cartaJogada.Nome}\" só pode ser usada em duelo.")
        {
        }
    }
}