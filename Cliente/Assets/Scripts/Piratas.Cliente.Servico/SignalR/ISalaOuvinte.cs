namespace Piratas.Cliente.Servico
{
    using Protocolo.Sala.Servidor;

    public interface ISalaOuvinte
    {
        void AoCriar(MensagemSalaServidor mensagemSalaServidor);

        void AoSair(MensagemSalaServidor mensagemSalaServidor);

        void AoEntrar(MensagemSalaServidor mensagemSalaServidor);

        void AoIniciarPartida(MensagemSalaServidor mensagemSalaServidor);
    }
}
