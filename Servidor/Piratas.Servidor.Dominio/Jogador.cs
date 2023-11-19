namespace Piratas.Servidor.Dominio;

using System;
using System.Collections.Generic;
using System.Linq;
using Cartas;
using Cartas.Tesouro;
using Cartas.Tripulacao;

public class Jogador
{
    public string Id { get; }

    public int AcoesDisponiveis { get; private set; }

    public Mao Mao { get; }

    public Campo Campo { get; }

    public Jogador(
        string id,
        Action<string, Carta> aoAdicionarCartaNaMao,
        Action<string, Carta> aoRemoverCartaNaMao,
        Action<string, Carta> aoAdicionarCartaNoCampo,
        Action<string, Carta> aoRemoverCartaNoCampo)
    {
        Id = id;
        Mao = new Mao(new List<Carta>());
        Campo = new Campo();

        Mao.AoAdicionar += AoAdicionarCartaNaMao;
        Mao.AoRemover += AoRemoverCartaNaMao;
        Campo.AoAdicionar += AoAdicionarCartaNoCampo;
        Campo.AoRemover += AoRemoverCartaNoCampo;

        void AoAdicionarCartaNaMao(Carta carta) =>
            aoAdicionarCartaNaMao?.Invoke(Id, carta);

        void AoRemoverCartaNaMao(Carta carta) =>
            aoRemoverCartaNaMao?.Invoke(Id, carta);

        void AoAdicionarCartaNoCampo(Carta carta) =>
            aoAdicionarCartaNoCampo?.Invoke(Id, carta);

        void AoRemoverCartaNoCampo(Carta carta) =>
            aoRemoverCartaNoCampo?.Invoke(Id, carta);
    }

    public void ResetarAcoesDisponiveis(int acoes)
    {
        AcoesDisponiveis = acoes;
    }

    public void SubtrairAcoesDisponiveis()
    {
        AcoesDisponiveis--;
    }

    public int CalcularTesouros()
    {
        int tesouros = 0;

        tesouros += _obterTesourosMeioAmuleto();
        tesouros += _obterTesourosMao();
        tesouros += _obterTesourosProtegidos();
        tesouros += _obterTesourosPiratasNobres();

        return tesouros;
    }

    public override string ToString() => Id;

    public override bool Equals(object obj) => Equals(obj as Jogador);

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Jogador jogador1, Jogador jogador2)
    {
        if (ReferenceEquals(jogador1, jogador2))
            return true;

        if (ReferenceEquals(jogador1, null))
            return false;

        if (ReferenceEquals(jogador2, null))
            return false;

        return jogador1.Equals(jogador2);
    }

    public static bool operator !=(Jogador jogador1, Jogador jogador2) => !(jogador1 == jogador2);

    private bool Equals(Jogador outroJogador)
    {
        if (ReferenceEquals(outroJogador, null))
            return false;

        if (ReferenceEquals(outroJogador, this))
            return true;

        return Id == outroJogador.Id;
    }

    private int _obterTesourosPiratasNobres()
    {
        int tesourosPiratasNobres =
            Campo.Tripulacao.Where(t => t is PirataNobre).Sum(t => ((PirataNobre)t).Tesouros);

        return tesourosPiratasNobres;
    }

    private int _obterTesourosProtegidos()
    {
        IEnumerable<Tesouro> tesourosProtegidos = Campo.ObterTodasProtegidas().OfType<Tesouro>();

        int somaTesourosProtegidos = tesourosProtegidos.Sum(c => c.Valor);

        return somaTesourosProtegidos;
    }

    private int _obterTesourosMao()
    {
        List<Tesouro> tesourosMao = Mao.ObterTodas<Tesouro>();

        int somaTesourosMao = 0;

        foreach (Tesouro tesouro in tesourosMao)
        {
            if (tesouro is MeioAmuleto)
                continue;

            somaTesourosMao = tesourosMao.Sum(c => c.Valor);
        }

        return somaTesourosMao;
    }

    private int _obterTesourosMeioAmuleto()
    {
        List<MeioAmuleto> meiosAmuletos = Mao.ObterTodas<MeioAmuleto>();

        int somaMeiosAmuletos = MeioAmuleto.CalcularPontosTesouro(meiosAmuletos);

        return somaMeiosAmuletos;
    }
}
