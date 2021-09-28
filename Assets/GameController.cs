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
        //if (instances.field[cardToAdd.tag].Count == 10)
        //{
        //    distanceBetweenCards = 0.5f;
        //    Debug.Log("Lol");
        //    for (int i = 1; i < instances.field[cardToAdd.tag].Count - 1; i++)
        //    {
        //        GameObject card = instances.field[cardToAdd.tag][i];
        //        Debug.Log("Position1" + instances.field[cardToAdd.tag][i].transform.position);
        //        instances.field[cardToAdd.tag][i].transform.position =
        //            new Vector3(card.transform.position.x, card.transform.position.y + 0.5f, card.transform.position.z);
        //        Debug.Log("Position2" + instances.field[cardToAdd.tag][i].transform.position);
        //    }
        //}
        GameObject previousCard = instances.field[cardToAdd.tag][instances.field[cardToAdd.tag].Count - 1];
        Vector3 cardPosition = previousCard.transform.position;
        cardPosition.y -= distanceBetweenCards;
        cardPosition.z -= 1;
        Debug.Log("Add " + cardToAdd.name + " to deck" + cardToAdd.tag);
        cardToAdd.transform.position = cardPosition;
        instances.field[cardToAdd.tag].Add(cardToAdd);
    }
    public void RemoveCardFromRow(GameObject card)
    {
        instances.field[card.tag].Remove(card);
    }
}
