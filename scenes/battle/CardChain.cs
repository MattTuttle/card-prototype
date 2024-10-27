using Godot;
using Game.Scenes.Card;

public partial class CardChain : PanelContainer
{
    private HBoxContainer cardChain;

    public override void _Ready()
    {
        cardChain = GetNode<HBoxContainer>("%CardChain");
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return data.Obj is Card;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        data.As<Card>().MoveTo(cardChain);
    }

}
