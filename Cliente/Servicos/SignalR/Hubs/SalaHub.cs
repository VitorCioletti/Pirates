namespace Piratas.Cliente.Servicos.Hubs;

using System;
using Protocolo.Sala.Servidor;

public class SalaHub
{
    public void AoCriar(MensagemSalaServidor mensagemSalaServidor)
    {
        Console.WriteLine("Chegou coisinhaa");
        Console.WriteLine(mensagemSalaServidor.IdSala);
    }

    public void AoSair(MensagemSalaServidor mensagemSalaServidor)
    {

    }

    public void AoEntrar(MensagemSalaServidor mensagemSalaServidor)
    {
    }

    public void AoIniciarPartida(MensagemSalaServidor mensagemSalaServidor)
    {
    }
}
