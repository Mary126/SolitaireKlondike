using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDeckController : MonoBehaviour
{
    public Instances instances;
    public GameController gameController;
    private int TopDeckCount;
    public GameObject deckPlace;
    private void Start()
    {
        TopDeckCount = instances.topDeck.Count;
    }
    private void OnMouseDown()
    {
        OpenCardTopDeck(instances.card);
    }
    void RemoveCardFromOpenDeck()
    {
        instances.topDeckOpenPosition.z += 1;
    }
    void OpenCardTopDeck(GameObject card)
    {
        if (TopDeckCount > 0)
        {
            
            card = Instantiate(instances.card);
            instances.topDeckOpenPosition.z = -instances.field["DeckOpened"].Count;
            card.transform.position = instances.topDeckOpenPosition;
            card.GetComponent<SpriteRenderer>().sprite = instances.topDeck[TopDeckCount - 1];
            card.GetComponent<CardController>().gameController = gameController;
            card.name = "card" + TopDeckCount;
            TopDeckCount--;
            if (TopDeckCount == 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = instances.CardBlank;
            }
            card.tag = "DeckOpened";
            instances.field["DeckOpened"].Add(card);
        }
        else
        {
            for (int i = 0; i < instances.field["DeckOpened"].Count; i++)
            {
                TopDeckCount++;
                Destroy(instances.field["DeckOpened"][i]);
            }
            for (int i = 0; i < instances.field["DeckOpened"].Count; i++)
            {
                instances.field["DeckOpened"].RemoveAt(i);
            }
            this.GetComponent<SpriteRenderer>().sprite = instances.CardBack;
        }
    }
}
