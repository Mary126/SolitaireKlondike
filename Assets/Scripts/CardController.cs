using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Instances instances;
    private Plane dragPlane;
    private Vector3 offset;
    private Camera myMainCamera;
    private Vector3 startingPosition;
    public bool collidedWithRow = false;
    private bool moveOtherCards = false;
    private bool isDragging = false;
    public GameObject isMovingWithOtherCard = null; //if this card is moving with the card underneath
                                                    //i decided to make it a gameobject
    private GameObject cardAbove;
    public KlondikeRules klondikeRules;
    private CardInstances cardInstances;
    
    void Start()
    {
        cardInstances = GetComponent<CardInstances>();
        myMainCamera = Camera.main;
    }
    
    public void OnMouseDown()
    {
        if (cardInstances.isOpen) // if the card is open
        {
            dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
            Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            offset = transform.position - camRay.GetPoint(planeDist);
            startingPosition = transform.position;
            // if there is a card above this one
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 2f))
            {
                moveOtherCards = true; //this card is moving the other card
                cardAbove = hit.collider.gameObject;
                cardAbove.GetComponent<CardController>().isMovingWithOtherCard = gameObject;
                cardAbove.BroadcastMessage("OnMouseDown");
            }
        }
    }
    public void OnMouseDrag()
    {
        if (cardInstances.isOpen) // if the card is open
        {
            Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            Vector3 positionToMove = camRay.GetPoint(planeDist) + offset;
            if (positionToMove.x != startingPosition.x && positionToMove.y != startingPosition.y)
            {
                isDragging = true;
                positionToMove.z = -60 + startingPosition.z; //move above all cards
                if (isMovingWithOtherCard != null)
                    positionToMove.y = isMovingWithOtherCard.transform.position.y - 1;
                transform.position = positionToMove;
            }
            // if the card is moving the card above, broadcast function on that card
            if (moveOtherCards)
            {
                cardAbove.BroadcastMessage("OnMouseDrag");
            }
        }
    }
    public void OnMouseUp()
    {
        if (isDragging && cardInstances.isOpen) // if the card was dragged and that card is open
        {
            if (isMovingWithOtherCard != null) // if this card is moving with the card underneath
            {
                if (collidedWithRow) // if the first card of the moving column is colliding with a row
                {
                    klondikeRules.RemoveCardFromRow(gameObject.GetComponent<CardInstances>());
                    cardInstances.position.row = isMovingWithOtherCard.GetComponent<CardInstances>().position.row;
                    cardInstances.position.number = isMovingWithOtherCard.GetComponent<CardInstances>().position.number;
                    tag = isMovingWithOtherCard.tag;
                    klondikeRules.PutCardInRow(gameObject.GetComponent<CardInstances>());
                }
                else // if not return to the starting position
                {
                    transform.position = startingPosition;
                }
            }
            else
            {
                RaycastHit hit;
                for (int i = 0; i < 4; i++) // i decided to make four rays just to be sure
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f) && !collidedWithRow)
                    {
                        // if the colliding place is not a top deck place
                        // and the row has been changed
                        if (hit.collider.gameObject.tag != "DeckOpened" && 
                             hit.collider.gameObject.tag != "Deck Card" && 
                             tag != hit.collider.tag) 
                        {
                            string rowType = hit.collider.gameObject.tag;
                            if (klondikeRules.CompatibleWithCard(gameObject, 
                                                                 hit.collider.gameObject,
                                                                 rowType.Substring(0, rowType.Length - 1))) 
                            {
                                klondikeRules.RemoveCardFromRow(gameObject.GetComponent<CardInstances>());
                                tag = rowType; //make the tag of the object the tag of the collided object
                                //take the row name from the tag
                                cardInstances.position.row = rowType.Substring(0, rowType.Length - 1);
                                //take the number of the row from tag
                                cardInstances.position.number = int.Parse(rowType.Substring(rowType.Length - 1, 1));
                                klondikeRules.PutCardInRow(gameObject.GetComponent<CardInstances>());
                                collidedWithRow = true; // this card has been collided with a row
                            }
                        }
                    }
                }
                // if the card hasn't collided with anything return to the starting position
                if (collidedWithRow == false) 
                {
                    Debug.Log("ReturnToPosition" + startingPosition);
                    transform.position = startingPosition; //return to the starting position
                }
            }
        }
        if (moveOtherCards) // if the card is moving other card
        {
            cardAbove.GetComponent<CardController>().collidedWithRow = collidedWithRow; // show that card that we collided with a row
            cardAbove.BroadcastMessage("OnMouseUp");
        }
        moveOtherCards = false;
        collidedWithRow = false;
        isMovingWithOtherCard = null;
        isDragging = false;
    }
}
