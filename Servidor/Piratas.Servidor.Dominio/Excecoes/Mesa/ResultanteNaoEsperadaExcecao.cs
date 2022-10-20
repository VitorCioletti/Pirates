namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    using Dominio.Acoes.Resultante.Base;

    public class ResultanteNaoEsperadaExcecao : BaseMesaExcecao
    {
        public BaseResultante BaseResultante { get; private set; }

        public ResultanteNaoEsperadaExcecao(BaseResultante baseResultante)
            : base("resultante-nao-esperada", $"Resultante \"{baseResultante.Id}\" não esperada.")

        {
            BaseResultante = baseResultante;
        }
    }
}
