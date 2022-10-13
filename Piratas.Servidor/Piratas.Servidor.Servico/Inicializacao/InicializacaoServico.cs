namespace Piratas.Servidor.Servico.Inicializacao
{
    using WebSocket;
    using Configuracao = Configuracao.Configuracao;
    using Log = Log.Log;

    public static class InicializacaoServico
    {
        public static void Inicializar()
        {
            Configuracao.Inicializar();
            Log.Inicializar();
            WebSocket.Inicializar();
        }
    }
}
