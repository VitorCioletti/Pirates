namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    public abstract class Imediata : BaseResultante
    {
        public Imediata(Acao origem, Jogador realizador, Jogador alvo = null) : base(origem, realizador, alvo)
        {

        }
    }
}
