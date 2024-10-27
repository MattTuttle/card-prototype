using Godot;
using Game.Resources.Card;
using Game.Resources.Deck;
using Game.Scenes.Card;

namespace Game.Battle;

public partial class Hand : PanelContainer
{
    [Export]
    public DeckResource cardDeck;
    [Export]
    public PackedScene cardScene;

    private HBoxContainer handContainer;

    public override void _Ready()
    {
        handContainer = GetNode<HBoxContainer>("%HandContainer");

        Callable.From(() => {
                OnDrawCardPressed();
            }).CallDeferred();

        var drawCardButton = GetNode<Button>("%DrawCardButton");
        drawCardButton.Pressed += OnDrawCardPressed;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return data.Obj is Card;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        data.As<Card>().MoveTo(handContainer);
    }

    public void PlaceCardInHand(CardResource cardResource)
    {
        var card = cardScene.Instantiate<Card>();
        handContainer.AddChild(card);
        card.AssignResource(cardResource);
    }

    private void OnDrawCardPressed()
    {
        PlaceCardInHand(cardDeck.DrawCard());
    }
}
