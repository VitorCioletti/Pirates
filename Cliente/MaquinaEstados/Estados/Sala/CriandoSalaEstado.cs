namespace Piratas.Cliente.MaquinaEstados.Estados.Sala
{
    using System;
    using Protocolo.Sala.Servidor;
    using Servicos;

    public class CriandoSalaEstado : BaseEstado
    {
        public CriandoSalaEstado(MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
        }

        public override void Inicializar()
        {
            Console.WriteLine("Criando sala...");

            SalaServico.CriarSala();
        }

        public override void AoCriarSala(MensagemSalaServidor mensagemSalaServidor)
        {
            MaquinaEstados.Trocar(new SalaEstado(mensagemSalaServidor, MaquinaEstados));
        }
    }
}
