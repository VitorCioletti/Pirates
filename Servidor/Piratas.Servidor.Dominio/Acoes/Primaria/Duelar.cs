namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas.Duelo;
    using Cartas.Extensao;
    using Cartas.Tipos;
    using Excecoes.Acoes;
    using Resultante;
    using Resultante.Enums;

    public class Duelar : BasePrimaria
    {
        public Duelo CartaIniciadora { get; private set; }

        public Duelar(Jogador realizador, Jogador alvo, Duelo cartaIniciadora) : base(realizador, alvo) =>
            CartaIniciadora = cartaIniciadora;

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            List<Canhao> cartasCanhao = Realizador.Mao.ObterTodas<Canhao>();

            if (cartasCanhao.Count == 0)
                throw new NaoPossuiCartaDueloExcecao(this);

            mesa.EntrarModoDuelo();

            var escolherCartaIniciadoraDuelo = new EscolherCanhaoIniciadorDuelo(
                this,
                Realizador,
                TipoEscolha.Carta,
                cartasCanhao.ObterIds()
            );

            var acoesResultantes = new List<BaseAcao> {escolherCartaIniciadoraDuelo};

            return acoesResultantes;
        }
    }
}
