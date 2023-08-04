namespace Piratas.Cliente.MaquinaEstados.Estados.Partida;

using System;
using Servicos;

public class PartidaEstado : BaseEstado
{
    private readonly Guid _idSala;

    public PartidaEstado(Guid idSala, MaquinaEstados maquinaEstados) : base(maquinaEstados)
    {
        _idSala = idSala;
    }

    public override void Inicializar()
    {
        SalaServico.IniciarPartida(_idSala);

    }
}
