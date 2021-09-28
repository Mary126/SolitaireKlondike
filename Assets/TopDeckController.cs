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
        Debug.Log(TopDeckCount);
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
            instances.topDeckOpenPosition.z -= 1;
            card.transform.position = instances.topDeckOpenPosition;
            card.GetComponent<SpriteRenderer>().sprite = instances.topDeck[TopDeckCount - 1];
            card.GetComponent<CardController>().gameController = gameController;
            card.name = "card" + TopDeckCount;
            TopDeckCount--;
            Debug.Log("Opened Top Deck Card " + TopDeckCount);
            if (TopDeckCount == 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = instances.CardBlank;
            }
            card.tag = "Deck Card";
            instances.topDeckOpened.Add(card);
        }
        else
        {
            for (int i = 0; i < instances.topDeckOpened.Count; i++)
            {
                TopDeckCount++;
                Destroy(instances.topDeckOpened[i]);
            }
            for (int i = 0; i < instances.topDeckOpened.Count; i++)
            {
                instances.topDeckOpened.RemoveAt(i);
            }
            this.GetComponent<SpriteRenderer>().sprite = instances.CardBack;
        }
    }
}
