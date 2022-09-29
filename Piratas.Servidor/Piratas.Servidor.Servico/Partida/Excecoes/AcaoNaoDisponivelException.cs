namespace Piratas.Servidor.Servico.Excecoes
{
    public class AcaoNaoDisponivelException : BaseServicoException
    {
        public string IdAcao { get; private set; }

        public AcaoNaoDisponivelException(string idAcao) :
            base("acao-nao-disponivel", $"Ação de id \"{idAcao}\" não disponível.")
        {
            IdAcao = idAcao;
        }
    }
}
