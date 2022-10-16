namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public class AcaoNaoDisponivelExcecao : BasePartidaExcecao
    {
        public string IdAcao { get; private set; }

        public AcaoNaoDisponivelExcecao(string idAcao) :
            base("acao-nao-disponivel", $"Ação de id \"{idAcao}\" não disponível.")
        {
            IdAcao = idAcao;
        }
    }
}
