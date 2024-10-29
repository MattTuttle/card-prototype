using Godot;
using Game.Component;

namespace Game.Battle;

public partial class BattleScene : Node
{
    [Signal]
    public delegate void EndTurnEventHandler();

    private Hand hand;
    private CardChain cardChain;
    private HealthComponent playerHealthComponent;
    private HealthComponent enemyHealthComponent;

    public override void _Ready()
    {
        hand = GetNode<Hand>("%HandContainer");
        cardChain = GetNode<CardChain>("%CardChainContainer");

        playerHealthComponent = GetNode<HealthComponent>("Player/HealthComponent");
        enemyHealthComponent = GetNode<HealthComponent>("Enemy/HealthComponent");

        cardChain.OnChainExecuted += OnChainExecuted;
    }

    private void OnEndTurn()
    {
        hand.DrawToHandLimit();
    }

    private void OnChainExecuted()
    {
        if (enemyHealthComponent.IsAlive) {
            if (hand.NumCardsInHand == 0) {
                playerHealthComponent.Damage(2); // really stupid enemy damage
                EmitSignal(SignalName.EndTurn);
                hand.DrawToHandLimit();
            }
        } else {
            // enemy is dead!
            GD.Print("Battle won!");
        }
    }

}
