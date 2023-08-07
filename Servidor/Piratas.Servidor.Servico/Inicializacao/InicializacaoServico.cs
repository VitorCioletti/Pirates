namespace Piratas.Servidor.Servico.Inicializacao
{
    using Configuracao;
    using Log;
    using Partida;
    using SignalR;

    public static class InicializacaoServico
    {
        public static void Inicializar()
        {
            ConfiguracaoServico.ObterDadosArquivoConfiguracao();
            LogServico.ConfigurarLogger();
            PartidaServico.ConfigurarGeradorCartas();
            SignalRServico.ConfigurarSignalR();
        }
    }
}
