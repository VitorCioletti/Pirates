namespace Piratas.Cliente.MaquinaEstados
{
    using System;
    using System.Collections.Generic;
    using Estados;
    using Protocolo.Sala.Servidor;
    using Servicos;

    public class MaquinaEstados : ISalaOuvinte
    {
        private readonly Stack<BaseEstado> _estados;

        public MaquinaEstados()
        {
            _estados = new Stack<BaseEstado>();
        }

        public void Adicionar(BaseEstado estado)
        {
            _imprimirEstado(estado);

            _estados.Push(estado);

            estado.Inicializar();
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

        public void AoCriar(MensagemSalaServidor mensagemSalaServidor) =>
            ObterAtual().AoCriarSala(mensagemSalaServidor);

        public void AoSair(MensagemSalaServidor mensagemSalaServidor) => ObterAtual().AoSairSala(mensagemSalaServidor);

        public void AoEntrar(MensagemSalaServidor mensagemSalaServidor) =>
            ObterAtual().AoEntrarSala(mensagemSalaServidor);

        public void AoIniciarPartida(MensagemSalaServidor mensagemSalaServidor) =>
            ObterAtual().AoIniciarPartida(mensagemSalaServidor);
    }
}
