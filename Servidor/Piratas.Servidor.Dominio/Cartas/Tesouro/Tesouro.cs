namespace Piratas.Servidor.Dominio.Cartas.Tesouro
{
    using System.Collections.Generic;
    using Acoes;

    public class Tesouro : Carta
    {
        public int Valor { get; protected set; }

        protected Tesouro(int valor) => Valor = valor;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}
