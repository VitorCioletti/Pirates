namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    using Dominio;
    using System.Collections.Generic;

    public class DanificarEmbarcacao : BaseImediata
    {
        public DanificarEmbarcacao(Jogador realizador) : base(realizador)
        {
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            Realizador.Campo.DanificarEmbarcacao();

            return null;
        }
    }
}
