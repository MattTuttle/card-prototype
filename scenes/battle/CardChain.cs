using Godot;
using Game.Scenes.Card;
using Game.Resources.Card;
using Game.Component;

public partial class CardChain : PanelContainer
{
    [Signal]
    public delegate void OnChainExecutedEventHandler();

    [Export]
    private HealthComponent enemyHealthComponent;
    [Export]
    private HealthComponent playerHealthComponent;

    private HBoxContainer cardChain;
    private Button executeButton;

    public override void _Ready()
    {
        cardChain = GetNode<HBoxContainer>("%CardChain");
        executeButton = GetNode<Button>("%ExecuteButton");

        executeButton.Pressed += OnExecutePressed;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return data.Obj is Card;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        data.As<Card>().MoveTo(cardChain);
    }

    private void ClearCards()
    {
        foreach (var child in cardChain.GetChildren())
        {
            cardChain.RemoveChild(child);
        }
    }

    private void ExecuteCardAction(CardActionType action, int power)
    {
        switch (action)
        {
            case CardActionType.Attack:
                enemyHealthComponent.Damage(power);
                GD.Print($"Swing at enemy for {power}");
                break;
            case CardActionType.Heal:
                playerHealthComponent.Heal(power);
                GD.Print($"Heal for {power}");
                break;
        }
        EmitSignal(SignalName.OnChainExecuted);
    }

    private void OnExecutePressed()
    {
        int power = 0;
        var cards = cardChain.GetChildren();
        if (cards.Count == 0) return;

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] is Card card)
            {
                power += card.CardResource.Power;
                if (i == cards.Count - 1)
                {
                    // last card
                    ExecuteCardAction(card.CardResource.FinalAction, power);
                }
            }
        }
        ClearCards();
    }

}
