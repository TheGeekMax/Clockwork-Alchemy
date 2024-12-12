using System;
using UnityEngine;
using System.Collections.Generic;

public class ConvoyerManager : MonoBehaviour, IManager{
    public static ConvoyerManager instance;
    private List<Cell> reprs;
    
    public GameObject convoyerPrefab;
    public EditedUnion grid;
    public GameObject[,] conveyors;
    public void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void Initialize(){
        grid = new EditedUnion(5, 5);
        conveyors = new GameObject[5, 5];
        for (int i = 0; i < 5; i ++){
            for (int j = 0; j < 5; j ++){
                conveyors[i, j] = null;
            }
        }
        reprs = new List<Cell>();
    }
    
    private float time = 0;
    public void Update(){
        //detect click
        if (Input.GetMouseButtonDown(0)){
            //get mouse position
            reprs = grid.getRepresentatives();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = new Vector2Int((int)mousePos.x, (int)mousePos.y);
            
            if (gridPos.x < 0 || gridPos.x >= 5 || gridPos.y < 0 || gridPos.y >= 5){
                return;
            }
            if (conveyors[gridPos.x, gridPos.y] != null){
                conveyors[gridPos.x, gridPos.y].GetComponent<IConveyer>().onClicked(gridPos);
                return;
            }
            
            //instantiating the convoyer
            GameObject convoyer = Instantiate(convoyerPrefab, new Vector3(gridPos.x, gridPos.y, 0), Quaternion.identity);
            conveyors[gridPos.x, gridPos.y] = convoyer;
            grid.addCell(gridPos,0);
        }

        time += Time.deltaTime;
        if (time >= .5f){
            List<Cell> nexts = new List<Cell>();
            foreach(Cell c in reprs){
                conveyors[c.position.x, c.position.y].GetComponent<ConvoyerShow>().ShowD();
                Cell n = c.next;
                conveyors[n.position.x, n.position.y].GetComponent<ConvoyerShow>().ShowD();
                nexts.Add(n);
            }
            reprs = nexts;
            time -= .5f;
        }
    }

    public Vector2Int getReprCoordinate(Vector2Int pos){
        return grid.getRepresentative(pos).position;
    }

    public void OnDrawGizmos(){
        for (int i = 0; i < 5; i ++){
            for (int j = 0; j < 5; j ++){
                Gizmos.DrawWireCube(new Vector3(i+.5f, j+.5f, 0), Vector3.one);
            }
        }
    }
}
