namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using System;
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class Canhao : Duelo
    {
        public int Tiros { get; private set; }

        public Canhao()
        {
            var random = new Random();

            Tiros = random.Next(1, 2);
        }

        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            Campo campoRealizador = acao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}
