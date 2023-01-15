namespace Piratas.Cliente.MaquinaEstados.Estados
{
    using System;
    using System.Threading;
    using Protocolo.Sala.Servidor;
    using Sala;
    using Sala.CriandoSala;
    using Sala.EntrandoSala;

    public class MenuEstado : BaseEstado
    {
        public MenuEstado(MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
        }

        public override void Inicializar()
        {
            _imprimirMenu();
        }

        public override void AoReceberTexto(string texto)
        {
            if (int.TryParse(texto, out int operacao))
            {
                Console.WriteLine("Apenas números são permitidos.");

                Thread.Sleep(500);

                Console.Clear();

                _imprimirMenu();

                return;
            }

            var operacaoMenu = (OperacaoMenu)operacao;

            switch (operacaoMenu)
            {
                case OperacaoMenu.CriarSala:
                    MaquinaEstados.Adicionar(new CriandoSalaEstado(MaquinaEstados));
                    break;

                case OperacaoMenu.EntrarSala:
                    MaquinaEstados.Adicionar(new AguardandoEntradaTextoEstado("Digite o ID da sala.", MaquinaEstados));
                    break;

                default:
                    Console.WriteLine($"Operação \"{operacao}\"inválida. Digite novamente.");
                    break;
            }
        }

        public override void AoVoltarNoTopo(BaseResultadoEstado resultadoEstado)
        {
            switch (resultadoEstado)
            {
                case AguardandoEntradaTextoResultadoEstado aguardandoEntradaTextoResultadoEstado:
                    string idSala = aguardandoEntradaTextoResultadoEstado.Texto;

                    MaquinaEstados.Adicionar(new EntrandoSalaEstado(idSala, MaquinaEstados));
                    break;

                case CriandoSalaResultadoEstado criandoSalaResultado:
                    _tentarAbrirSala(criandoSalaResultado.MensagemSalaServidor);
                    break;

                case EntrandoSalaResultadoEstado entrandoSalaResultadoEstado:
                    _tentarAbrirSala(entrandoSalaResultadoEstado.MensagemSalaServidor);
                    break;

                case ErroResultadoEstado:
                    _imprimirMenu();
                    break;
            }
        }

        private void _tentarAbrirSala(MensagemSalaServidor mensagemSalaServidor)
        {
            if (!mensagemSalaServidor.PossuiErro)
            {
                MaquinaEstados.Adicionar(new SalaEstado(mensagemSalaServidor, MaquinaEstados));
            }
            else
            {
                var erroEstado = new ErroEstado(mensagemSalaServidor, MaquinaEstados);

                MaquinaEstados.Adicionar(erroEstado);
            }
        }

        private void _imprimirMenu()
        {
            Console.WriteLine("Menu");

            Console.WriteLine($"{(int)OperacaoMenu.EntrarSala} - Entrar Sala");
            Console.WriteLine($"{(int)OperacaoMenu.CriarSala} - Criar Sala");
        }
    }
}