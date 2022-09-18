namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Excecoes.Cartas;
    using System.Collections.Generic;
    using Tipos;

    public class Timoneiro : Duelo
    {
        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, Mesa mesa)
        {
            if (!(acao is DescerCartasDuelo))
                throw new CartaRespostaDueloException(this);

            mesa.SairModoDuelo();

            yield return null;
        }
    }
}
