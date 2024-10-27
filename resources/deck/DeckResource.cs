using Godot;
using System.Collections.Generic;
using Game.Resources.Card;

namespace Game.Resources.Deck;

[GlobalClass]
public partial class DeckResource : Resource
{
    [Export]
    public CardResource[] cardsInDeck;

    private Stack<CardResource> deckStack = new();

    public void Shuffle()
    {
        foreach (var card in cardsInDeck)
        {
            deckStack.Push(card);
        }
    }

    public CardResource DrawCard()
    {
        if (deckStack.Count == 0)
        {
            Shuffle();
        }
        return deckStack.Pop();
    }

}
