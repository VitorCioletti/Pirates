namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class TripulanteNaoPodeSerAfogadoExcecao : BaseAcoesExcecao
    {
        private const string _idExcecao = "tripulante-nao-pode-ser-afogado";

        public TripulanteNaoPodeSerAfogadoExcecao(BaseAcao acao, string idTripulante)
            : base(acao, _idExcecao, $"O tripulante \"{idTripulante}\" não pode ser afogado.")
        {
        }

        public TripulanteNaoPodeSerAfogadoExcecao(BaseAcao acao, string idTripulante, string idCartaRazaoAfogamento)
            : base(
                acao,
                _idExcecao,
                $"O tripulante \"{idTripulante}\" não pode ser afogado por \"{idCartaRazaoAfogamento}\".")
        {
        }
    }
}
