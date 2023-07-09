namespace Piratas.Servidor.Servico.Inicializacao
{
    using Configuracao;
    using Log;
    using SignalR;

    public static class InicializacaoServico
    {
        public static void Inicializar()
        {
            ConfiguracaoServico.Inicializar();
            LogServico.Inicializar();
            SignalRServico.Inicializar();
        }
    }
}
