namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;

    public class Tesouro : Carta
    {
        public int Valor { get; protected set; }

        public Tesouro(int valor) => Valor = valor;

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}
