namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;

    public class Tesouro : Carta
    {
        public int Valor { get; protected set; }

        public Tesouro(int valor) => Valor = valor;

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}
