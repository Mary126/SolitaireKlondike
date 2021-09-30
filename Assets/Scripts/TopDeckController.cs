using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TopDeckController : MonoBehaviour
{
    public Instances instances;
    public KlondikeRules klondikeRules;
    private int TopDeckCount;
    public GameObject deckPlace;
    private void Start()
    {
        TopDeckCount = instances.cards.Count;
    }
    private void OnMouseDown()
    {
        OpenCardTopDeck();
    }
    void OpenCardTopDeck()
    {
        if (instances.cards.Count > 0) // if there are cards in the top deck
        {
            GameObject card = Instantiate(instances.card);
            card.transform.SetParent(instances.cardPlaces.transform);
            instances.topDeckOpenPosition.z = -instances.field["DeckOpened"].Count;
            card.transform.position = instances.topDeckOpenPosition;
            card.GetComponent<SpriteRenderer>().sprite = instances.cards[instances.cards.Count - 1];
            card.GetComponent<CardInstances>().SetCardSuit(instances.cards[instances.cards.Count - 1]);
            card.GetComponent<CardInstances>().isOpen = true;
            card.GetComponent<CardController>().klondikeRules = klondikeRules;
            card.GetComponent<CardController>().instances = instances;
            card.GetComponent<CardInstances>().SetPosition("DeckCard", 0);
            card.name = "card" + card.GetComponent<CardInstances>().cardSuit.suit + card.GetComponent<CardInstances>().cardSuit.number;
            card.tag = "DeckOpened";
            instances.cards.RemoveAt(instances.cards.Count - 1);
            if (instances.cards.Count == 0) // if there are no cards in the deck
            {
                //set the sprite to blank
                GetComponent<SpriteRenderer>().sprite = instances.CardBlank;
            }
            instances.field["DeckOpened"].Add(card);
        }
        else // if there are not, then
        {
            //destroy all the cards in DeckOpened
            for (int i = instances.field["DeckOpened"].Count - 1; i >= 0; i--) 
            {
                instances.cards.Add(instances.field["DeckOpened"][i].GetComponent<SpriteRenderer>().sprite);
                Destroy(instances.field["DeckOpened"][i]);
            }
            instances.field["DeckOpened"].Clear();
            //set the sprite to the back of the card
            GetComponent<SpriteRenderer>().sprite = instances.CardBack;
            var query = instances.cards.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
            Debug.Log(query.Count);
        }
    }
}
