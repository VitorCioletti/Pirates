namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class ProibidoDescerCartaExcecao : BaseAcoesExcecao
    {
        public ProibidoDescerCartaExcecao(BaseAcao baseAcao, Carta carta)
            : base(baseAcao, "proibido-descer-carta", $"Probido jogar cartas do tipo \"{carta.Id}\".")
        {
        }
    }
}
