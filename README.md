# Servidor Piratas

## TODOs

### Essenciais

- Alterar o sistema de ação para retornar uma fila de ações para um jogador. O resultado da última ação é entregue como
parâmetro da próxima.
- Fazer ações primárias retornarem uma resultante para que o jogador consiga escolher o que precisa. A classe jogador deve possuir um m
    método que retorna a lista da ações primárias.
- Escrever testes de unidade.
- Enviar resposta para as diferentes sessões dos jogadores. Pesquisar propriedade Sessions nos controladores.
- Fazer captura de exceções em todos os controladores em um só lugar.
- Resolver como avisar quando uma carta de evento for comprada.
- Resolver como avisar o cliente que foi um Bau Armadilha na carta Saque.
- Resolver a verificação se todas as ações resposta de duelo foram realizadas em SairModoDuelo.
- Vossa alteza é randômico ou permite escolha do jogador?

### Adequado fazer

- Definir como setar Turno em BaseAcao.
- Criar entidade BaseAcaoComAlvo para evitar que ações que não possuam alvo tenham acesso a propriedade Alvo.
- Verificar se a quantidade mínima de jogadores foi atingida no gerenciador de partida.
- Implementar líder da sala que pode iniciar a partida.
- Fechamento gracioso após recebimento de sinal?

### Futuro bem bem distante

- Otimizar crição de cartas. Ao invés do jogar possuir uma instância da carta, ele deveria possuir uma string com o id
  da carta. A mesa pode ter uma lista estática com as cartas, toda vez que alguma carta precisa ser executada, a
  entidade pede a instância daquela carta e a executa na mesa.
