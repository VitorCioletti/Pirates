namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class ProibidoDescerCartaExcecao : BaseAcoesExcecao
    {
        public ProibidoDescerCartaExcecao(Acao acao, Carta carta)
            : base(acao, "proibido-descer-carta", $"Probido jogar cartas do tipo \"{carta.Id}\".")
        {
        }
    }
}
