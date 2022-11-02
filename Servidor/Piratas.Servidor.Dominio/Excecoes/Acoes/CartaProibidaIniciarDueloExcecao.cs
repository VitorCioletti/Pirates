namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class CartaProibidaIniciarDueloExcecao : BaseAcoesExcecao
    {
        public CartaProibidaIniciarDueloExcecao(BaseAcao baseAcao, Carta carta)
            : base(baseAcao, "carta-proibida-iniciar-duelo", $"Carta \"{carta.Id}\" não pode inicar duelo.")
        {
        }
    }
}
