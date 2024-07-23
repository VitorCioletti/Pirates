### Must do

- Implement data reception on the test client.
- Sending error packets in SignalR.
- Statically typed SignalR.
- The GetCurrentTableState function is only used to return the initial state of the table, change to return field data and pending actions in case of reconnection.
- Change the action system to return a queue of actions to a player. The result of the last action is delivered as
next parameter.
- Make primary actions return a result so that the player can choose what they need. The player class must have a m
 method that returns the list of primary actions.
- Write unit tests.
- Catch exceptions in all controllers in one place.
- Solve how to notify when an event card is drawn.
- Resolve how to notify the customer that it was a Bau Trap on the Loot card.
- Resolve the check whether all duel response actions were performed in ExitDuelMode.
- Your highness, is it random or does it allow player choice?

### Interesting to do

- Define how to set Shift in BaseAction.
- Create BaseActionWithTarget entity to prevent actions that do not have a target from accessing the Target property.
- Check whether the minimum number of players has been reached in the match manager.
- Implement room leader who can start the match.
- Graceful closing after receiving a signal?

### Very very distant future

- Test the insertion of a state machine into the action system.
- Persist valid matches and run them on the server again to ensure the server continues to process the rules as it should.
- Optimize card creation. Instead of the player having an instance of the card, he should have a string with the id
 of the letter. The table can have a static list of cards, every time a card needs to be played, the
 entity asks for the instance of that card and executes it on the table.
