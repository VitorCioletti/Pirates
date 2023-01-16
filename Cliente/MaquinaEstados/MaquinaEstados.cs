namespace Piratas.Cliente.MaquinaEstados
{
    using System;
    using System.Collections.Generic;
    using Estados;

    public class MaquinaEstados
    {
        private Stack<BaseEstado> _estados;

        public MaquinaEstados()
        {
            _estados = new Stack<BaseEstado>();
        }

        public void Adicionar(BaseEstado estado)
        {
            _imprimirEstado(estado);

            estado.Inicializar();

            _estados.Push(estado);
        }

        public void Trocar(BaseEstado estado)
        {
            BaseEstado estadoAtual = ObterAtual();

            Remover(estadoAtual);
            Adicionar(estado);
        }

        public void Remover(BaseEstado estado)
        {
            BaseResultadoEstado resultadoEstado = estado.Limpar();

            _estados.Pop();

            BaseEstado estadoAtual = ObterAtual();

            _imprimirEstado(estadoAtual);

            estadoAtual.AoVoltarNoTopo(resultadoEstado);
        }

        public BaseEstado ObterAtual()
        {
            return _estados.Peek();
        }

        private void _imprimirEstado(BaseEstado estado)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Estado atual: \"{estado.GetType().Name}\".");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
