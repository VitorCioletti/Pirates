namespace Piratas.Cliente.MaquinaEstados.Estados.Sala;

using System;
using Protocolo.Sala.Servidor;
using Servicos;

public class EntrandoSalaEstado : BaseEstado
{
    public EntrandoSalaEstado(MaquinaEstados maquinaEstados) : base(maquinaEstados)
    {
    }

    public override void Inicializar()
    {
        MaquinaEstados.Adicionar(new AguardandoEntradaTextoEstado("Digite o ID da sala.", MaquinaEstados));
    }

    public override void AoVoltarNoTopo(BaseResultadoEstado resultadoEstado)
    {
        switch (resultadoEstado)
        {
            case AguardandoEntradaTextoResultadoEstado aguardandoEntradaTextoResultadoEstado:
                string idSala = aguardandoEntradaTextoResultadoEstado.Texto;

                SalaServico.EntrarSala(idSala);
                Console.WriteLine($"Tentando entrar na sala \"{idSala}\".");

                break;
        }
    }

    public override void AoEntrarSala(MensagemSalaServidor mensagemSalaServidor)
    {
        MaquinaEstados.Trocar(new SalaEstado(mensagemSalaServidor, MaquinaEstados));
    }
}
