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
    public GameObject isMovingWithOtherCard = null;
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
        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
        startingPosition = transform.position;
        // if there is a card above this one
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 100f))
        {
            moveOtherCards = true;
            cardAbove = hit.collider.gameObject;
            cardAbove.GetComponent<CardController>().isMovingWithOtherCard = gameObject;
            cardAbove.BroadcastMessage("OnMouseDown");
        }
    }
    public void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        Vector3 positionToMove = camRay.GetPoint(planeDist) + offset;
        if (positionToMove.x == startingPosition.x && positionToMove.y == startingPosition.y)
        {
            isDragging = false;
        }
        else 
        {
            isDragging = true;
            positionToMove.z = -60 + startingPosition.z; //move above all cards
            if (isMovingWithOtherCard != null)
                positionToMove.y = isMovingWithOtherCard.transform.position.y - 1;
            transform.position = positionToMove;
            
        }
        if (moveOtherCards)
        {
            cardAbove.BroadcastMessage("OnMouseDrag");
        }
    }
    public void OnMouseUp()
    {
        if (isDragging)
        {
            if (isMovingWithOtherCard != null)
            {
                if (collidedWithRow)
                {
                    klondikeRules.RemoveCardFromRow(gameObject.GetComponent<CardInstances>());
                    cardInstances.position.row = isMovingWithOtherCard.GetComponent<CardInstances>().position.row;
                    cardInstances.position.number = isMovingWithOtherCard.GetComponent<CardInstances>().position.number;
                    tag = isMovingWithOtherCard.tag;
                    klondikeRules.PutCardInRow(gameObject.GetComponent<CardInstances>());
                }
                else
                {
                    transform.position = startingPosition;
                }
            }
            else
            {
                RaycastHit hit;
                for (int i = 0; i < 4; i++)
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f) && !collidedWithRow)
                    {
                        if (hit.collider.gameObject.tag != "DeckOpened" && hit.collider.gameObject.tag != "Deck Card" && tag != hit.collider.tag)
                        {
                            string rowType = hit.collider.gameObject.tag;
                            if (klondikeRules.CompatibleWithCard(gameObject, hit.collider.gameObject, rowType.Substring(0, rowType.Length - 1))) {
                                klondikeRules.RemoveCardFromRow(gameObject.GetComponent<CardInstances>());
                                tag = rowType;
                                cardInstances.position.row = rowType.Substring(0, rowType.Length - 1);
                                cardInstances.position.number = int.Parse(rowType.Substring(rowType.Length - 1, 1));
                                klondikeRules.PutCardInRow(gameObject.GetComponent<CardInstances>());
                                collidedWithRow = true;
                            }
                        }
                    }
                }
                if (collidedWithRow == false)
                {
                    Debug.Log("ReturnToPosition" + startingPosition);
                    transform.position = startingPosition;
                }
            }
        }
        if (moveOtherCards)
        {
            cardAbove.GetComponent<CardController>().collidedWithRow = collidedWithRow;
            cardAbove.BroadcastMessage("OnMouseUp");
        }
        moveOtherCards = false;
        collidedWithRow = false;
        isMovingWithOtherCard = null;
        isDragging = false;
    }
}
