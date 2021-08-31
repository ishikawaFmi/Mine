using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    [SerializeField]
    public Text view = null;

    [SerializeField]
    public GameObject mine = null;

    [SerializeField]
    public GameObject chek = null;
    [SerializeField]
    public GameObject sec = null;

    [SerializeField]
    private CellState.CellStates cellState = CellState.CellStates.None;

    [SerializeField]
    private MineSweeper mineSweeper;

    public int x, y;

    
    public CellState.CellStates CellStates
    {
        get => cellState;
        set
        {
            cellState = value;
            OnCellStateChanged();
        }
    }

    private void OnValidate()
    {
        OnCellStateChanged();
        view.enabled = false;
        mineSweeper = GameObject.Find("MIneSweeper").GetComponent<MineSweeper>();
    }
    private void OnCellStateChanged()
    {
        if (view == null) return;

        if (cellState == CellState.CellStates.None)
        {
            view.text = "";
        }
        else
        {
            view.text = ((int)cellState).ToString();
            view.color = Color.blue;
        }
    }
  
    
    public void Clicked()
    {
        if (cellState == CellState.CellStates.Mine && Input.GetMouseButtonUp(0) && mine.activeSelf == false)
        {
            mine.SetActive(true);
            mineSweeper.mine.Remove(this);
            mineSweeper.gameOver = true;
        }
        else if(Input.GetMouseButtonUp(0)&&mine.activeSelf == false)
        {
            if (mineSweeper.z)
            {
                mineSweeper.z = false;
                mineSweeper.CreateMine(this);
               
            }
            view.enabled = true;
            chek.SetActive(false);
            sec.SetActive(true);
            if (cellState==CellState.CellStates.None)
            {
                mineSweeper.Open(this);
            }
           
        }
        if (Input.GetMouseButtonUp(1)&& view.enabled == false && mine.activeSelf == false)
        {
            chek.SetActive(true);
        }
    }
    
}
