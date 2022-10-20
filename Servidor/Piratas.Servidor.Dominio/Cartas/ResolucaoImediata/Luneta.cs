namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class Luneta : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            List<string> idsCartasMaoAlvo = acao.Alvo.Mao.ObterTodas().Select(c => c.Id.ToString()).ToList();

            var descartarCarta = new DescartarCarta(acao, acao.Realizador, acao.Alvo, idsCartasMaoAlvo);
            var acoesResultantes = new List<Acao> {descartarCarta};

            return acoesResultantes;
        }
    }
}
