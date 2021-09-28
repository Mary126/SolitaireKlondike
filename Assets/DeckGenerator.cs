using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DeckGenerator : MonoBehaviour
{
    Instances instance;
    void Awake()
    {
        instance = GetComponent<Instances>();
    }
    public void GenerateDeck()
    {
        System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
        instance.topDeck = instance.cards.OrderBy(x => rand.Next()).ToList(); //shuffle existing sprites
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
