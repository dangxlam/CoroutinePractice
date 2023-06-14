using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private GameObject cellHover;
    private GameObject cellSelected;
    
    private ECellColor cellColor;
    

    public ECellColor CellColor 
    { 
        get { return cellColor; }
        set { 
            cellColor = value;

            switch (cellColor)
            {
                case ECellColor.BLACK:
                    GetComponent<Renderer>().material = ResourcesCTL.Instance.BlackCellMaterial;
                    break;
                default:
                    GetComponent<Renderer>().material = ResourcesCTL.Instance.WhiteCellMaterial;
                    break;
            }
        }
    }

    private ECellState cellState;
    public ECellState CellState { 
        get { return cellState; } 
        set 
        {  
            cellState = value;

            switch (cellState)
            {
                case ECellState.NORMAL:
                    cellHover.SetActive(false);
                    cellSelected.SetActive(false);
                    break;
                case ECellState.POINTED:
                    cellSelected.SetActive(false);
                    cellHover.SetActive(true);
                    break;
                case ECellState.SELECTED:
                    cellHover.SetActive(false);
                    cellSelected.SetActive(true);
                    break;
                case ECellState.MOVEABLE:
                    cellHover.SetActive(true);
                    break;
                default :
                    cellHover.SetActive(false);
                    cellSelected.SetActive(false);
                    break;
            }
        } 
    }

    private void Start()
    {
        cellHover = this.transform.GetChild(0).gameObject;
        cellSelected = this.transform.GetChild(1).gameObject;
        CellState = ECellState.NORMAL;
    }

   

    public float SIZE
    {
        get
        {
            return GetComponent<Renderer>().bounds.size.x;
        }
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log("Cell Selected");
    //    SetCellState(ECellState.SELECTED);
    //}

   

    public void SetCellState(ECellState cellState)
    {
        if(cellState != ECellState.SELECTED  )
        {
            //if (cellState == ECellState.NORMAL)
            //    this.CellState = cellState;

            //else
            if (this.CellState != ECellState.SELECTED)
            {
                this.CellState = cellState;
            }

        } else
        {
            if (this.CellState == ECellState.SELECTED)
                this.CellState = ECellState.NORMAL;
            else 
                this.CellState = ECellState.SELECTED;
        }
        
    }




}
