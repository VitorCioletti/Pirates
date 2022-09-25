namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class CartaProibidaIniciarDuelo : BaseAcoesException
    {
        public Carta Carta { get; private set; }

        public CartaProibidaIniciarDuelo(Acao acao, Carta carta)
            : base(acao, "carta-proibida-iniciar-duelo", $"Carta \"{carta.Id}\" não pode inicar duelo.")
        {
            Carta = carta;
        }
    }
}
