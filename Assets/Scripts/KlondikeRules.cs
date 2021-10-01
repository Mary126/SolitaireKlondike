using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlondikeRules : MonoBehaviour
{
    private Instances instances;
    public float distanceBetweenCards = 1;

    void Awake()
    {
        instances = GetComponent<Instances>();
        instances.klondikeGenerator.GenerateDeck();
    }
    public bool CompatibleWithCard(GameObject card, GameObject previousCard, string rowType)
    {
        CardInstances previousCardInstance;
        // if we can get CardInstances component from previousCard
        if (previousCard.TryGetComponent<CardInstances>(out previousCardInstance))
        {
            if (previousCard.GetComponent<CardInstances>().isOpen) //if that card is open
            {
                CardInstances cardInstance = card.GetComponent<CardInstances>();
                // if rowType is bottom row, the card is not the same color of the suit suit as the card underneath
                // and card number is smaller than the card number underneath by one
                // i.e a black jack will go on a red queen, a red three will go on a black four
                if (rowType == "BottomRow" && 
                    (cardInstance.cardSuit.suit == "hearts" || cardInstance.cardSuit.suit == "diamonds") &&
                    (previousCardInstance.cardSuit.suit == "spades" || previousCardInstance.cardSuit.suit == "clubs") &&
                    cardInstance.cardSuit.number == previousCardInstance.cardSuit.number - 1)
                {
                    return true;
                }
                if (rowType == "BottomRow" &&
                    (cardInstance.cardSuit.suit == "spades" || cardInstance.cardSuit.suit == "clubs") &&
                    (previousCardInstance.cardSuit.suit == "hearts" || previousCardInstance.cardSuit.suit == "diamonds") &&
                    cardInstance.cardSuit.number == previousCardInstance.cardSuit.number - 1)
                {
                    return true;
                }
                // if rowType is top row, the card is the same suit as the card underneath
                // and card number is bigger than the card number underneath by one
                // i.e a queen of hearts will go on a jack of hearts, a two of spades will go on an ace of spades
                if (rowType == "TopRow" && cardInstance.cardSuit.suit ==
                    previousCardInstance.cardSuit.suit && cardInstance.cardSuit.number == previousCardInstance.cardSuit.number + 1)
                {
                    return true;
                }
            }
        }
        else 
        {
            // if row place type is BottomRow and the card we want to put is a King
            if (rowType == "BottomRow" && card.GetComponent<CardInstances>().cardSuit.number == 13)
            {
                return true;
            }
            // if row place type is TopRow and the card we want to put is an Ace
            if (rowType == "TopRow" && card.GetComponent<CardInstances>().cardSuit.number == 1)
            {
                return true;
            }
        }
        return false;
    }
    public void PutCardInRow(CardInstances cardToAdd)
    {
        if (cardToAdd.position.row == "TopRow")
        {
            List<GameObject> row = instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()];
            GameObject previousCard = row[row.Count - 1];
            row.Add(cardToAdd.gameObject); // add the card to a row
            // make the card's position previous card's posiotion with a z -= 1
            cardToAdd.transform.position = new Vector3( previousCard.transform.position.x, 
                                                        previousCard.transform.position.y, 
                                                        previousCard.transform.position.z - 1);
            IsPlayerWinning();
        }
        else if (cardToAdd.position.row == "BottomRow")
        {
            List<GameObject> row = instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()];
            cardToAdd.transform.position = new Vector3(row[row.Count - 1].transform.position.x,
                                                       row[row.Count - 1].transform.position.y,
                                                       row[row.Count - 1].transform.position.z - 1);
            row.Add(cardToAdd.gameObject);
            if (row.Count < 14) distanceBetweenCards = 1f;
            if (row.Count >= 14)
            {
                distanceBetweenCards = 0.6f;
            }
            for (int i = 1; i < row.Count; i++)
            {
                GameObject card = row[i];
                GameObject prCard = row[i - 1];
                CardInstances prCardInstances;
                if (prCard.TryGetComponent<CardInstances>(out prCardInstances))
                {
                    if (prCardInstances.isOpen)
                    {
                        card.transform.position =
                            new Vector3(card.transform.position.x,
                                        prCard.transform.position.y - distanceBetweenCards,
                                        card.transform.position.z);
                    }
                }
            }
        }
    }
    public void RemoveCardFromRow(CardInstances cardToRemove)
    {
        instances.field[cardToRemove.tag].Remove(cardToRemove.gameObject);
        if (cardToRemove.GetComponent<CardInstances>().position.row == "BottomRow")
        {
            List<GameObject> row = instances.field[cardToRemove.position.row + cardToRemove.position.number.ToString()];
            CardInstances prCard;
            //if the previous object is a card (try to get CardInstances from that object)
            if(row[instances.field[cardToRemove.position.row + cardToRemove.position.number.ToString()].Count - 1].TryGetComponent<CardInstances>(out prCard))
            {
                if(!prCard.isOpen) //if that card is not open, open that card
                {
                    prCard.gameObject.GetComponent<SpriteRenderer>().sprite = prCard.cardSuit.sprite;
                    prCard.isOpen = true;
                } 
            }
        }
    }
    private void IsPlayerWinning()
    {
        int columns = 0;
        for (int i = 1; i <= 4; i++)
        {
            Debug.Log(instances.field["TopRow" + i.ToString()].Count);
            if (instances.field["TopRow" + i.ToString()].Count == 14) columns++;
        }
        if (columns == 4)
        {
            Time.timeScale = 0;
            instances.winScreen.SetActive(true);
        }
    }
}
