using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Instances instances;
    public float distanceBetweenCards = 1;

    void Awake()
    {
        instances = GetComponent<Instances>();
    }
    
    public void PutCardInRow(GameObject cardToAdd)
    {
        if (instances.field[cardToAdd.tag].Count < 10) distanceBetweenCards = 1f;
        if (instances.field[cardToAdd.tag].Count >= 12)
        {
            distanceBetweenCards = 0.5f;
        }
        for (int i = 1; i < instances.field[cardToAdd.tag].Count; i++)
        {
            GameObject card = instances.field[cardToAdd.tag][i];
            instances.field[cardToAdd.tag][i].transform.position =
                new Vector3(card.transform.position.x, instances.field[cardToAdd.tag][i - 1].transform.position.y - distanceBetweenCards, card.transform.position.z);
        }
        GameObject previousCard = instances.field[cardToAdd.tag][instances.field[cardToAdd.tag].Count - 1];
        Vector3 cardPosition = previousCard.transform.position;
        cardPosition.y -= distanceBetweenCards;
        cardPosition.z -= 1;
        cardToAdd.transform.position = cardPosition;
        instances.field[cardToAdd.tag].Add(cardToAdd);
    }
    public void RemoveCardFromRow(GameObject card)
    {
        instances.field[card.tag].Remove(card);
    }
}
