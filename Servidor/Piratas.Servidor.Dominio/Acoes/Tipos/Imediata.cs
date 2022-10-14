namespace Piratas.Servidor.Dominio.Acoes.Tipos
{
    public abstract class Imediata : Resultante
    {
        public Imediata(Acao origem, Jogador realizador, Jogador alvo = null) : base(origem, realizador, alvo)
        {

        }
    }
}
