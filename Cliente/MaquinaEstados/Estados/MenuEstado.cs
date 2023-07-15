namespace Piratas.Cliente.MaquinaEstados.Estados
{
    using System;
    using System.Threading;
    using Sala;

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
            if (!int.TryParse(texto, out int operacao))
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
                    MaquinaEstados.Adicionar(new EntrandoSalaEstado(MaquinaEstados));

                    break;

                default:
                    Console.WriteLine($"Operação \"{operacao}\"inválida. Digite novamente.");

                    break;
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
