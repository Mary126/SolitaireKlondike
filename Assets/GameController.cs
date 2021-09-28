using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Instances instances;
    private DeckGenerator deckGenerator;

    void Awake()
    {
        instances = GetComponent<Instances>();
        deckGenerator = GetComponent<DeckGenerator>();
        deckGenerator.GenerateDeck();
    }
    
    public void PutCardInBottomRow(GameObject cardToAdd, int row)
    {
        if (row >= 0 && row <= 6)
        {
            GameObject previousCard = instances.bottomRow[row][instances.bottomRow[row].Count - 1];
            Vector3 cardPosition = previousCard.transform.position;
            cardPosition.y -= 0.8f;
            cardPosition.z -= 1;
            cardToAdd.transform.position = cardPosition;
            instances.bottomRow[row].Add(cardToAdd);
        }
    }
    public void RemoveCardFromRow(GameObject card)
    {
        for (int i = 0; i < 6; i++)
        {
            instances.bottomRow[i].Remove(card);
        }
        //Debug.Log("Called Remover");
    }
}
