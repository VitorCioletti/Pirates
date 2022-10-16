namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    using Dominio.Acoes.Tipos;

    public class ResultanteNaoEsperadaExcecao : BaseMesaExcecao
    {
        public Resultante Resultante { get; private set; }

        public ResultanteNaoEsperadaExcecao(Resultante resultante)
            : base("resultante-nao-esperada", $"Resultante \"{resultante.Id}\" não esperada.")

        {
            Resultante = resultante;
        }
    }
}
