namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public abstract class Canhao : Duelo
    {
        public int Tiros { get; private set; }

        public Canhao(int tiros) => Tiros = tiros;

        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Campo campoRealizador = baseAcao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}
