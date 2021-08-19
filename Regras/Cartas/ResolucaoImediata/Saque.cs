namespace Piratas.Servidor.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Passivo;
    using Tipos;
    using System.Collections.Generic;

    public class Saque : ResolucaoImediata
    {
        public Saque(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa _) => 
            _aplicarEfeito(acao.Realizador.Mao, acao.Alvo.Mao);

        internal IEnumerable<Resultante> _aplicarEfeito(Mao maoRealizador, Mao maoAlvo)
        {
            // TODO: Como avisar o cliente que foi um Bau Armadilha?
            (Mao maoSaqueador, Mao maoSaqueado) = maoAlvo.Possui<BauArmadilha>() ?
                (maoAlvo, maoRealizador) : (maoRealizador, maoAlvo);

            var cartaSaqueada = maoSaqueado.ObterQualquer();

            maoSaqueado.Remover(cartaSaqueada);
            maoSaqueador.Adicionar(cartaSaqueada);

            yield return null;
        }
    }
}