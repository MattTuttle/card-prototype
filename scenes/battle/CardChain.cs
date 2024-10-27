using Godot;
using Game.Scenes.Card;

public partial class CardChain : PanelContainer
{
    private HBoxContainer cardChain;
    private Button attackButton;

    public override void _Ready()
    {
        cardChain = GetNode<HBoxContainer>("%CardChain");
        attackButton = GetNode<Button>("%AttackButton");

        attackButton.Pressed += OnAttackPressed;
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

    private void OnAttackPressed()
    {
        int power = 0;
        foreach (var child in cardChain.GetChildren())
        {
            if (child is Card card)
            {
                power += card.CardResource.Power;
            }
        }
        GD.Print($"Attack for {power}");
        ClearCards();
    }

}
