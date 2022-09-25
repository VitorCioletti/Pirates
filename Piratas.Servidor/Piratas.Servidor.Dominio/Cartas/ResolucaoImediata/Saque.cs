namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;
    using Passivo;
    using Tipos;

    public class Saque : ResolucaoImediata
    {
        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa _) =>
            _aplicarEfeito(acao.Realizador.Mao, acao.Alvo.Mao);

        internal IEnumerable<Resultante> _aplicarEfeito(Mao maoRealizador, Mao maoAlvo)
        {
            // TODO: Como avisar o cliente que foi um Bau Armadilha?
            var (maoSaqueador, maoSaqueado) =
                maoAlvo.Possui<BauArmadilha>() ? (maoAlvo, maoRealizador) : (maoRealizador, maoAlvo);

            var cartaSaqueada = maoSaqueado.ObterQualquer();

            maoSaqueado.Remover(cartaSaqueada);
            maoSaqueador.Adicionar(cartaSaqueada);

            yield return null;
        }
    }
}
