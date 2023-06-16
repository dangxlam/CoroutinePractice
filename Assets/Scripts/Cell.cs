using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private GameObject cellHover;
    private GameObject cellSelected;
    
    private ECellColor cellColor;
    public BaseChessPieces CurrentPiece { get;  set; }

    public Vector2Int CellLocation { get; set; }

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
                    //CurrentPiece.BeSelected();
                    break;
                case ECellState.MOVEABLE:
                    cellSelected.SetActive(false);
                    cellHover.SetActive(true);
                    break;
                default :
                    cellHover.SetActive(false);
                    cellSelected.SetActive(false);
                    break;
            }
        } 
    }

    private void Awake()
    {
        cellHover = this.transform.GetChild(0).gameObject;
        cellSelected = this.transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        
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
        switch (cellState)
        {
            case ECellState.NORMAL:
                if (this.CellState != ECellState.SELECTED && this.CellState != ECellState.MOVEABLE)
                {
                    this.CellState = ECellState.NORMAL;
                }
                break;
            case ECellState.POINTED:
                if (this.CellState != ECellState.SELECTED && this.CellState != ECellState.MOVEABLE)
                {
                    this.CellState = cellState;
                }
                break;
            case ECellState.SELECTED:
                if(CurrentPiece != null)
                {
                    if (this.CellState == ECellState.SELECTED)
                    {
                        this.CellState = ECellState.NORMAL;
                    }
                    else
                        this.CellState = cellState;
                    
                }
                
                break;
            case ECellState.MOVEABLE:
                if (this.CellState == ECellState.MOVEABLE)
                {
                    this.CellState = ECellState.NORMAL;
                }
                else
                    this.CellState = cellState;
                
                break;
            default:
                
                break;
        }
       
    }

    public void SetChessPiece (BaseChessPieces chessPieces)
    {
        this.CurrentPiece = chessPieces;
    }


}
