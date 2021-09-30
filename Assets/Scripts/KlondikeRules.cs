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
        if (previousCard.TryGetComponent<CardInstances>(out _) && previousCard.GetComponent<CardInstances>().isOpen)
        {
            CardInstances cardInstance = card.GetComponent<CardInstances>();
            CardInstances previousCardInstance = previousCard.GetComponent<CardInstances>();
            if (rowType == "BottomRow" && cardInstance.info.type == previousCardInstance.info.type && cardInstance.info.number == previousCardInstance.info.number - 1)
            {
                return true;
            }
            if (rowType == "TopRow" && cardInstance.info.type == previousCardInstance.info.type && cardInstance.info.number == previousCardInstance.info.number + 1)
            {
                return true;
            }
        }
        else
        {
            if (rowType == "BottomRow" && card.GetComponent<CardInstances>().info.number == 13 && previousCard.GetComponent<CardInstances>().isOpen)
            {
                return true;
            }
            if (rowType == "TopRow" && card.GetComponent<CardInstances>().info.number == 1 && previousCard.GetComponent<CardInstances>().isOpen)
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
            instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()].Add(cardToAdd.gameObject);
            GameObject previousCard = 
                instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()][instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()].Count - 2];
            cardToAdd.transform.position = new Vector3(previousCard.transform.position.x, previousCard.transform.position.y, previousCard.transform.position.z - 1);
        }
        else if (cardToAdd.position.row == "BottomRow")
        {
            instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()].Add(cardToAdd.gameObject);
            if (instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()].Count < 10) distanceBetweenCards = 1f;
            if (instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()].Count >= 12)
            {
                distanceBetweenCards = 0.5f;
            }
            for (int i = 1; i < instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()].Count; i++)
            {
                GameObject card = instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()][i];
                GameObject prCard = instances.field[cardToAdd.position.row + cardToAdd.position.number.ToString()][i - 1];
                CardInstances prCardInstances;
                if (prCard.TryGetComponent<CardInstances>(out prCardInstances))
                {
                    if (prCardInstances.isOpen)
                    {
                        card.transform.position =
                            new Vector3(prCard.transform.position.x,
                                        prCard.transform.position.y - distanceBetweenCards,
                                        prCard.transform.position.z - 1);
                    }
                }
            }
        }
    }
    public void RemoveCardFromRow(CardInstances card)
    {
        instances.field[card.tag].Remove(card.gameObject);
        Debug.Log(card.GetComponent<CardInstances>().position.row);
        if (card.GetComponent<CardInstances>().position.row == "BottomRow")
        {
            Debug.Log("Puk");
            CardInstances prCard;
            if(instances.field[card.position.row + card.position.number.ToString()][instances.field[card.position.row + card.position.number.ToString()].Count - 1].TryGetComponent<CardInstances>(out prCard))
            {
                if(!prCard.isOpen)
                {
                    prCard.gameObject.GetComponent<SpriteRenderer>().sprite = prCard.info.sprite;
                } 
            }
        }
    }
}
