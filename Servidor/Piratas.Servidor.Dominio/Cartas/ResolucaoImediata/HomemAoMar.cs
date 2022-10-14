namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Tipos;

    public class HomemAoMar : ResolucaoImediata
    {
        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, acao.Alvo);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, Jogador alvo)
        {
            yield return new AfogarTripulante(acao, acao.Realizador, alvo);
        }
    }
}
