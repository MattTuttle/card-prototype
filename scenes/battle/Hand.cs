using Godot;
using Game.Resources.Card;
using Game.Resources.Deck;
using Game.Scenes.Card;

namespace Game.Battle;

public partial class Hand : PanelContainer
{
    [Signal]
    public delegate void EndTurnEventHandler();

    [Export]
    public int HandLimit = 5;
    [Export]
    private DeckResource cardDeck;
    [Export]
    private PackedScene cardScene;

    [Export]
    private HealthComponent playerHealthComponent;

    private HBoxContainer handContainer;

    public override void _Ready()
    {
        handContainer = GetNode<HBoxContainer>("%HandContainer");

        var drawCardButton = GetNode<Button>("%DrawCardButton");
        drawCardButton.Pressed += DrawCard;

        Callable.From(DrawToHandLimit).CallDeferred();
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

    public int NumCardsInHand => handContainer.GetChildren().Count;

    public void DrawToHandLimit()
    {
        for (int i = NumCardsInHand; i < HandLimit; i++)
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {
        PlaceCardInHand(cardDeck.DrawCard());
    }
}
