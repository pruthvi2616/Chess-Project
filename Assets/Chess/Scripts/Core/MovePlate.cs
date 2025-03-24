using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;


    // Board positions not World positions
    int matrixX;
    int matrixY;

    //false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            //Set to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Destroy the victim Chesspiece
        if (attack)
        {
            GameObject cp = controller.GetComponent<ChessPlayerPlacementHandler>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<ChessPlayerPlacementHandler>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<ChessPlayerPlacementHandler>().Winner("white");
            Destroy(cp);
        }

        //Set the Chesspiece's original location to be empty
        controller.GetComponent<ChessPlayerPlacementHandler>().SetPositionEmpty(reference.GetComponent<ChessBoardPlacementHandler>().GetXBoard(),
            reference.GetComponent<ChessBoardPlacementHandler>().GetYBoard());

        //Move reference chess piece to this position
        reference.GetComponent<ChessBoardPlacementHandler>().SetXBoard(matrixX);
        reference.GetComponent<ChessBoardPlacementHandler>().SetYBoard(matrixY);
        reference.GetComponent<ChessBoardPlacementHandler>().SetCoords();

        //Update the matrix
        controller.GetComponent<ChessPlayerPlacementHandler>().SetPosition(reference);


        //Switch Current Player
        controller.GetComponent<ChessPlayerPlacementHandler>().NextTurn();

        //Destroy the move plates including self
        reference.GetComponent<ChessBoardPlacementHandler>().DestroyMovePlates();

    }
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
    


}