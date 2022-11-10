namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Passivo;

    public class Saque : BaseResolucaoImediata
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Mao maoAlvo = baseAcao.Alvo.Mao;
            Mao maoRealizador = baseAcao.Realizador.Mao;

            (Mao maoSaqueador, Mao maoSaqueado) =
                maoAlvo.Possui<BauArmadilha>() ? (maoAlvo, maoRealizador) : (maoRealizador, maoAlvo);

            Carta cartaSaqueada = maoSaqueado.ObterQualquer();

            maoSaqueado.Remover(cartaSaqueada);
            maoSaqueador.Adicionar(cartaSaqueada);

            return null;
        }
    }
}
