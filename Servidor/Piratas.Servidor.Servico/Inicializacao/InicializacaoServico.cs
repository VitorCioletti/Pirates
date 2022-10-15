namespace Piratas.Servidor.Servico.Inicializacao
{
    using Configuracao;
    using Log;
    using WebSocket;

    public static class InicializacaoServico
    {
        public static void Inicializar()
        {
            ConfiguracaoServico.Inicializar();
            LogServico.Inicializar();
            WebSocket.Inicializar();
        }
    }
}
