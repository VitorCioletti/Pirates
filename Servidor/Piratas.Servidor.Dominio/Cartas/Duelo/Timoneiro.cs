namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Excecoes.Cartas;
    using Tipos;

    public class Timoneiro : Duelo
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            if (acao is not DescerCartaRespostaDuelo)
                throw new ApenasCartaRespostaDueloExcecao(this);

            mesa.SairModoDuelo();

            return null;
        }
    }
}
