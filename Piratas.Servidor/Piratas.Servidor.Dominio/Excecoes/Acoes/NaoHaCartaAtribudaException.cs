namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class NaoHaCartaAtribudaException : BaseAcoesException
    {
        public NaoHaCartaAtribudaException(Acao acao, Jogador jogador)
            : base(acao, "nao-ha-carta-atribuda", $"Não há carta atribuída para jogador \"{jogador.Id}\".")
        {
        }
    }
}
