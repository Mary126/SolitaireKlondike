using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private Plane dragPlane;
    private Vector3 offset;
    private Camera myMainCamera;
    private Vector3 startingPosition;
    private bool isDragging = false;
    private bool moveOtherCards = false;
    private GameObject cardAbove;
    public GameController gameController;
    void Start()
    {
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
            cardAbove.BroadcastMessage("OnMouseDown");
        }
    }
    public void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        Vector3 positionToMove = camRay.GetPoint(planeDist) + offset;
        positionToMove.z = -53 + startingPosition.z; //move above all cards
        Debug.Log(startingPosition.z);
        transform.position = positionToMove;
        isDragging = true;
        if (moveOtherCards == true)
        {
            cardAbove.BroadcastMessage("OnMouseDrag");
        }
    }
    public void OnMouseUp()
    {
        if (isDragging)
        {
            RaycastHit hit;
            bool targetHit = false;
            for (int i = 0; i < 4; i++)
            {
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f) && !targetHit) {
                    if (hit.collider.gameObject.tag != "DeckOpened" && hit.collider.gameObject.tag != "Deck Card")
                    {
                        gameController.RemoveCardFromRow(this.gameObject);
                        this.tag = hit.collider.gameObject.tag;
                        gameController.PutCardInRow(this.gameObject);
                        isDragging = false;
                        targetHit = true;
                    }
                }
            }
            if (targetHit == false)
            {
                transform.position = startingPosition;
            }
            if (moveOtherCards)
            {
                cardAbove.BroadcastMessage("OnMouseUp");
            }

        }
        moveOtherCards = false;
    }
}
