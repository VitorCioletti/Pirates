namespace Piratas.Cliente.MaquinaEstados.Estados.Sala;

using System;
using System.Collections.Generic;
using System.Linq;
using Protocolo.Sala.Servidor;

public class SalaEstado : BaseEstado
{
    private readonly MensagemSalaServidor _mensagemSalaServidor;

    private readonly List<string> _jogadoresNaSala;

    public SalaEstado(
        MensagemSalaServidor mensagemSalaServidor,
        MaquinaEstados maquinaEstados) : base(maquinaEstados)
    {
        _mensagemSalaServidor = mensagemSalaServidor;
        _jogadoresNaSala = new List<string> {mensagemSalaServidor.IdJogadorRealizouAcao};
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

    public override void AoEntrarSala(MensagemSalaServidor mensagemSalaServidor)
    {
        _jogadoresNaSala.Add(mensagemSalaServidor.IdJogadorRealizouAcao);

        _imprimirDadosSala(mensagemSalaServidor);
    }

    private void _imprimirDadosSala(MensagemSalaServidor mensagemSalaServidor)
    {
        Console.WriteLine("Sala");

        Console.WriteLine($"Seu id: \"{mensagemSalaServidor.IdJogadorRealizouAcao}\".");
        Console.WriteLine($"Id da sala: \"{mensagemSalaServidor.IdSala}\".");
        Console.WriteLine();

        List<string> jogadores = _jogadoresNaSala.ToList();

        jogadores.Remove(mensagemSalaServidor.IdJogadorRealizouAcao);

        if (jogadores.Count == 0)
            Console.WriteLine("Só você está na sala");
        else
            Console.WriteLine($"Jogadores na sala: {string.Join("\n", jogadores)}");
    }
}
