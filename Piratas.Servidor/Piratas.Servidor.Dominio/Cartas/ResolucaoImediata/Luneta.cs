namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using Tipos;

    public class Luneta : ResolucaoImediata
    {
        public Luneta(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa _) => _aplicarEfeito(acao);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao)
        {
            yield return new DescartarCarta(acao, acao.Realizador, acao.Alvo);
        }
    }
}