namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Passivo;
    using Tipos;

    public class Saque : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Mao maoAlvo = acao.Alvo.Mao;
            Mao maoRealizador = acao.Realizador.Mao;

            // TODO: Como avisar o cliente que foi um Bau Armadilha?
            (Mao maoSaqueador, Mao maoSaqueado) =
                maoAlvo.Possui<BauArmadilha>() ? (maoAlvo, maoRealizador) : (maoRealizador, maoAlvo);

            Carta cartaSaqueada = maoSaqueado.ObterQualquer();

            maoSaqueado.Remover(cartaSaqueada);
            maoSaqueador.Adicionar(cartaSaqueada);

            return null;
        }
    }
}
