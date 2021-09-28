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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 10f))
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
        transform.position = camRay.GetPoint(planeDist) + offset;
        isDragging = true;
        if (moveOtherCards == true && isDragging)
        {
            cardAbove.BroadcastMessage("OnMouseDrag");
        }
    }
    public void SnapWithOtherCard(GameObject card)
    {
        this.transform.position = card.transform.position;
    }
    public void OnMouseUp()
    {
        Debug.Log(this.name + this.isDragging);
        if (isDragging)
        {
            RaycastHit hit;
            bool targetHit = false;
            for (int i = 0; i < 4; i++)
            {
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f) && !targetHit) {
                    gameController.RemoveCardFromRow(this.gameObject);
                    this.tag = hit.collider.gameObject.tag;
                    switch (hit.collider.gameObject.tag)
                    {
                        case "BottomRow1": gameController.PutCardInBottomRow(this.gameObject, 0); break;
                        case "BottomRow2": gameController.PutCardInBottomRow(this.gameObject, 1); break;
                        case "BottomRow3": gameController.PutCardInBottomRow(this.gameObject, 2); break;
                        case "BottomRow4": gameController.PutCardInBottomRow(this.gameObject, 3); break;
                        case "BottomRow5": gameController.PutCardInBottomRow(this.gameObject, 4); break;
                        case "BottomRow6": gameController.PutCardInBottomRow(this.gameObject, 5); break;
                        default: break;
                    }
                    isDragging = false;
                    targetHit = true;
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
