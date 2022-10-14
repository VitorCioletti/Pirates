namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class HomemAoMar : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Jogador alvo = acao.Alvo;

            var afogarTripulante = new AfogarTripulante(acao, acao.Realizador, alvo);
            var acoesResultantes = new List<Acao> { afogarTripulante };

            return acoesResultantes;
        }
    }
}
