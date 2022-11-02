namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class TripulanteNaoPodeSerAfogadoExcecao : BaseAcoesExcecao
    {
        private const string _idExcecao = "tripulante-nao-pode-ser-afogado";

        public TripulanteNaoPodeSerAfogadoExcecao(BaseAcao baseAcao, string idTripulante)
            : base(baseAcao, _idExcecao, $"O tripulante \"{idTripulante}\" não pode ser afogado.")
        {
        }

        public TripulanteNaoPodeSerAfogadoExcecao(BaseAcao baseAcao, string idTripulante, string idCartaRazaoAfogamento)
            : base(
                baseAcao,
                _idExcecao,
                $"O tripulante \"{idTripulante}\" não pode ser afogado por \"{idCartaRazaoAfogamento}\".")
        {
        }
    }
}
