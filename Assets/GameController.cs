using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Instances instances;

    void Awake()
    {
        instances = GetComponent<Instances>();
    }
    
    public void PutCardInRow(GameObject cardToAdd)
    {
        GameObject previousCard = instances.field[cardToAdd.tag][instances.field[cardToAdd.tag].Count - 1];
        Vector3 cardPosition = previousCard.transform.position;
        cardPosition.y -= 0.8f;
        cardPosition.z -= 1;
        cardToAdd.transform.position = cardPosition;
        instances.field[cardToAdd.tag].Add(cardToAdd);
        Debug.Log("Put Card " + cardToAdd.name + " to " + cardToAdd.tag);
    }
    public void RemoveCardFromRow(GameObject card)
    {
        instances.field[card.tag].Remove(card);
        Debug.Log("Called Remover");
    }
}
