namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using Tipos;

    public abstract class Canhao : Duelo
    {
        public int Tiros { get; private set; }

        public Canhao(int tiros) => Tiros = tiros;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Campo campoRealizador = acao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}