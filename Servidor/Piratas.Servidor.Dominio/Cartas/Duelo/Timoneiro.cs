namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Excecoes.Cartas;
    using Tipos;

    public class Timoneiro : Duelo
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            if (acao is not DescerCartasDuelo)
                throw new ApenasCartaRespostaDueloExcecao(this);

            mesa.SairModoDuelo();

            return null;
        }
    }
}
