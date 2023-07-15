namespace Piratas.Cliente.MaquinaEstados.Estados.Sala;

using System;
using Protocolo.Sala.Servidor;
using Servicos;

public class EntrandoSalaEstado : BaseEstado
{
    private readonly string _idSala;

    public EntrandoSalaEstado(string idSala, MaquinaEstados maquinaEstados) : base(maquinaEstados)
    {
        _idSala = idSala;
    }

    public override void Inicializar()
    {
        Console.WriteLine($"Tentando entrar na sala \"{_idSala}\".");

        SalaServico.EntrarSala(_idSala);
    }

    public override void AoEntrarSala(MensagemSalaServidor mensagemSalaServidor)
    {
        MaquinaEstados.Trocar(new SalaEstado(mensagemSalaServidor, MaquinaEstados));
    }
}
