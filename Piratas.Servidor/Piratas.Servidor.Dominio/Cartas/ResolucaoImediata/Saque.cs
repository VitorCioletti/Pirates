namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Passivo;
    using Tipos;
    using System.Collections.Generic;

    public class Saque : ResolucaoImediata
    {
        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa _) =>
            _aplicarEfeito(acao.Realizador.Mao, acao.Alvo.Mao);

        internal IEnumerable<Resultante> _aplicarEfeito(Mao maoRealizador, Mao maoAlvo)
        {
            // TODO: Como avisar o cliente que foi um Bau Armadilha?
            (var maoSaqueador, var maoSaqueado) = maoAlvo.Possui<BauArmadilha>() ?
                (maoAlvo, maoRealizador) : (maoRealizador, maoAlvo);

            var cartaSaqueada = maoSaqueado.ObterQualquer();

            maoSaqueado.Remover(cartaSaqueada);
            maoSaqueador.Adicionar(cartaSaqueada);

            yield return null;
        }
    }
}
