namespace Piratas.Cliente.MaquinaEstados.Estados.Sala.CriandoSala
{
    using System;
    using Protocolo.Sala.Servidor;
    using Servicos;

    public class CriandoSalaEstado : BaseEstado
    {
        private MensagemSalaServidor _resultadoCriacaoSala;

        public CriandoSalaEstado(MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
        }

        public override void Inicializar()
        {
            Console.WriteLine("Criando sala...");

            MensagemSalaServidor resultado = SalaServico.CriarSala();

            _resultadoCriacaoSala = resultado;
        }

        public override BaseResultadoEstado Limpar()
        {
            Console.Clear();

            return new CriandoSalaResultadoEstado(_resultadoCriacaoSala);
        }
    }
}
