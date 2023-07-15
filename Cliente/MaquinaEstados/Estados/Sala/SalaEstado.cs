namespace Piratas.Cliente.MaquinaEstados.Estados.Sala;

using System;
using System.Collections.Generic;
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
        if (_mensagemSalaServidor.PossuiErro)
        {
            Console.WriteLine("Erro ao entrar na sala:");
            Console.WriteLine(_mensagemSalaServidor.IdErro);
            Console.WriteLine(_mensagemSalaServidor.DescricaoErro);

            Remover();

            return;
        }

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

    public override void AoEntrarSala(MensagemSalaServidor mensagemSalaServidor)
    {
        _imprimirDadosSala(mensagemSalaServidor);
    }

    public override void AoSairSala(MensagemSalaServidor mensagemSalaServidor)
    {
        _imprimirDadosSala(mensagemSalaServidor);
    }

    private void _imprimirDadosSala(MensagemSalaServidor mensagemSalaServidor)
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
}
