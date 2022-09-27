namespace Piratas.Servidor.Dominio.Acoes.Tipos
{
    public abstract class Resultante : Acao
    {
        public Acao Origem { get; set; }

        public Resultante(
            Acao origem,
            Jogador realizador,
            Jogador alvo = null)
            : base(realizador, alvo) => Origem = origem;
    }
}
