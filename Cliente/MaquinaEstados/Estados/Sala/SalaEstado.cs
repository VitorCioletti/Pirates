namespace Piratas.Cliente.MaquinaEstados.Estados.Sala
{
    using System;
    using Protocolo.Sala.Servidor;

    public class SalaEstado : BaseEstado
    {
        private readonly MensagemSalaServidor _mensagemSalaServidor;

        public SalaEstado(
            MensagemSalaServidor mensagemSalaServidor,
            MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
            _mensagemSalaServidor = mensagemSalaServidor;
        }

        public override void Inicializar()
        {
            _imprimirDadosSala(_mensagemSalaServidor);
        }

        public override BaseResultadoEstado Limpar()
        {
            Console.Clear();

            return null;
        }

        public override void AoVoltarNoTopo(BaseResultadoEstado resultadoEstado)
        {
        }

        public override void AoReceberTexto(string texto)
        {
        }

        private void _imprimirDadosSala(MensagemSalaServidor mensagemSalaServidor)
        {
            Console.WriteLine("Sala");

            Console.WriteLine($"Seu id: \"{mensagemSalaServidor.IdJogadorRealizouAcao}\".");
            Console.WriteLine($"Id da sala: \"{mensagemSalaServidor.IdSala}\".");

            Console.WriteLine("Jogadores na sala: ");
        }
    }
}
