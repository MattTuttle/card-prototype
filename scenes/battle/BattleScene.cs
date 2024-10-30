using Godot;

namespace Game.Battle;

public partial class BattleScene : Node
{
    [Signal]
    public delegate void EndTurnEventHandler();

    private EnemyList enemyList;
    private Hand hand;
    private CardChain cardChain;
    private Actor player;

    public override void _Ready()
    {
        hand = GetNode<Hand>("%HandContainer");
        cardChain = GetNode<CardChain>("%CardChainContainer");

        player = GetNode<Actor>("Player");
        enemyList = GetNode<EnemyList>("EnemyList");

        cardChain.OnChainExecuted += OnChainExecuted;
    }

    private void OnEndTurn()
    {
        hand.DrawToHandLimit();
    }

    private void OnChainExecuted()
    {
        if (enemyList.AreAllDead) {
            GD.Print("Battle won!");
        } else {
            // is end of player turn?
            if (hand.NumCardsInHand == 0) {
                enemyList.TakeTurn();
                EmitSignal(SignalName.EndTurn);
                hand.DrawToHandLimit();
            }
        }
    }

}
