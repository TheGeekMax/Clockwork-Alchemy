using System.Collections.Generic;
using UnityEngine;

public class ConvoyerShow : MonoBehaviour, IConveyer{
    private int rotation;
    bool isOn = false;
    
    public int getConveyerRotation(){
        return 0;
    }
    
    public void onClicked(Vector2Int pos){
        rotation = (rotation + 1) % 4;
        ConvoyerManager.instance.grid.replace(pos, rotation);
    }

    public void ShowD(){
        isOn = !isOn;
    }
    
    
    public void OnDrawGizmos(){
        Vector2Int pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Debug.Log(pos);
        Vector2Int reprPos = ConvoyerManager.instance.getReprCoordinate(pos);
        int ind = (reprPos.x + reprPos.y) % 2;
        if (ind == 0){
            Gizmos.color = Color.red;
        }
        else{
            Gizmos.color = Color.magenta;
        }
        
        Gizmos.DrawCube(transform.position+ new Vector3(.5f,.5f,0), new Vector3(.8f, .8f, .8f));
        
        Gizmos.color = Color.blue;
        if (rotation == 0){
            Gizmos.DrawLine(transform.position + new Vector3(.5f, .5f, 0), transform.position + new Vector3(.5f, 1.5f, 0));
        }
        else if (rotation == 1){
            Gizmos.DrawLine(transform.position + new Vector3(.5f, .5f, 0), transform.position + new Vector3(1.5f, .5f, 0));
        }
        else if (rotation == 2){
            Gizmos.DrawLine(transform.position + new Vector3(.5f, .5f, 0), transform.position + new Vector3(.5f, -.5f, 0));
        }
        else if (rotation == 3){
            Gizmos.DrawLine(transform.position + new Vector3(.5f, .5f, 0), transform.position + new Vector3(-.5f, .5f, 0));
        }

        if (isOn){
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position + new Vector3(.5f, .5f, 0), new Vector3(.2f, .2f, .2f));
        }
    }
}
