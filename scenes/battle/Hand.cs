using Godot;
using System.Collections.Generic;
using Game.Resources.Card;
using Game.Scenes.Card;

namespace Game.Battle;

public partial class Hand : PanelContainer
{
    [Export]
    public CardResource[] cardsInDeck;
    [Export]
    public PackedScene cardScene;

    private HBoxContainer handContainer;
    private Stack<CardResource> deckStack = new();

    public override void _Ready()
    {
        handContainer = GetNode<HBoxContainer>("%HandContainer");
        Shuffle();
        Callable.From(() => {
            DrawCard();
            DrawCard();
            DrawCard();
            }).CallDeferred();
    }

    public void Shuffle()
    {
        foreach (var card in cardsInDeck)
        {
            deckStack.Push(card);
        }
    }

    public void DrawCard()
    {
        if (deckStack.Count == 0) return;
        var cardResource = deckStack.Pop();
        PlaceCardInHand(cardResource);
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return data.Obj is Card;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var card = data.As<Card>();
        card.GetParent().RemoveChild(card);
        handContainer.AddChild(card);
    }

    public void PlaceCardInHand(CardResource cardResource)
    {
        var card = cardScene.Instantiate<Card>();
        handContainer.AddChild(card);
        card.AssignResource(cardResource);
    }
}
