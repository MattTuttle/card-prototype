using Godot;
using Game.Scenes.Card;

public partial class CardChain : PanelContainer
{
    [Signal]
    public delegate void ExecuteChainEventHandler();

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

    private void OnExecutePressed()
    {
        int power = 0;
        foreach (var child in cardChain.GetChildren())
        {
            if (child is Card card)
            {
                power += card.CardResource.Power;
            }
        }
        //playerHealthComponent.Heal(power);
        enemyHealthComponent.Damage(power);
        ClearCards();
        EmitSignal(SignalName.ExecuteChain);
    }

}
