namespace Piratas.Cliente.MaquinaEstados.Estados.Sala;

using System;
using System.Collections.Generic;
using System.Threading;
using Partida;
using Protocolo.Sala.Servidor;
using Servicos;

public class SalaEstado : BaseEstado
{
    private MensagemSalaServidor _ultimaMensagemSalaServidor;

    public SalaEstado(
        MensagemSalaServidor ultimaMensagemSalaServidor,
        MaquinaEstados maquinaEstados) : base(maquinaEstados)
    {
        _ultimaMensagemSalaServidor = ultimaMensagemSalaServidor;
    }

    public override void Inicializar()
    {
        if (_ultimaMensagemSalaServidor.PossuiErro)
        {
            Console.WriteLine("Erro ao entrar na sala:");
            Console.WriteLine(_ultimaMensagemSalaServidor.IdErro);
            Console.WriteLine(_ultimaMensagemSalaServidor.DescricaoErro);

            Remover();

            return;
        }

        _imprimir(_ultimaMensagemSalaServidor);
    }

    public override void AoVoltarNoTopo(BaseResultadoEstado resultadoEstado)
    {
    }

    public override void AoReceberTexto(string texto)
    {
        if (!int.TryParse(texto, out int operacao))
        {
            Console.WriteLine("Apenas números são permitidos.");

            Thread.Sleep(500);

            _imprimir(_ultimaMensagemSalaServidor);

            return;
        }

        var operacaoSala = (OperacaoSala)operacao;

        switch (operacaoSala)
        {
            case OperacaoSala.IniciarPartida:
                MaquinaEstados.Trocar(new PartidaEstado(_ultimaMensagemSalaServidor.IdSala, MaquinaEstados));

                break;

            case OperacaoSala.Sair:
                SalaServico.SairSala();
                Remover();

                break;

            default:
                Console.WriteLine($"Operação \"{operacao}\"inválida. Digite novamente.");

                break;
        }
    }

    public override void AoEntrarSala(MensagemSalaServidor mensagemSalaServidor)
    {
        _ultimaMensagemSalaServidor = mensagemSalaServidor;

        _imprimir(mensagemSalaServidor);
    }

    public override void AoSairSala(MensagemSalaServidor mensagemSalaServidor)
    {
        _ultimaMensagemSalaServidor = mensagemSalaServidor;

        _imprimir(mensagemSalaServidor);
    }

    public override void AoIniciarPartida(MensagemSalaServidor mensagemSalaServidor)
    {
        MaquinaEstados.Trocar(new PartidaEstado(mensagemSalaServidor.IdSala, MaquinaEstados));
    }

    private void _imprimir(MensagemSalaServidor mensagemSalaServidor)
    {
        _imprimirDados(mensagemSalaServidor);
        _imprimirOpcoes();
    }

    private void _imprimirDados(MensagemSalaServidor mensagemSalaServidor)
    {
        Console.WriteLine("Sala");

        Console.WriteLine($"Seu id: \"{mensagemSalaServidor.IdJogadorRealizouAcao}\".");
        Console.WriteLine($"Id da sala: \"{mensagemSalaServidor.IdSala}\".");
        Console.WriteLine();

        List<string> jogadores = mensagemSalaServidor.Jogadores;

        if (jogadores.Count == 0)
            Console.WriteLine("Só você está na sala");
        else
            Console.WriteLine($"Jogadores na sala: {string.Join("\n", jogadores)}");
    }

    private void _imprimirOpcoes()
    {
        Console.WriteLine();
        Console.WriteLine($"{(int)OperacaoSala.IniciarPartida} - Iniciar Partida");
        Console.WriteLine($"{(int)OperacaoSala.Sair} - Sair");
    }
}
