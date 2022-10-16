namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class CartaProibidaIniciarDueloExcecao : BaseAcoesExcecao
    {
        public CartaProibidaIniciarDueloExcecao(Acao acao, Carta carta)
            : base(acao, "carta-proibida-iniciar-duelo", $"Carta \"{carta.Id}\" não pode inicar duelo.")
        {
        }
    }
}
