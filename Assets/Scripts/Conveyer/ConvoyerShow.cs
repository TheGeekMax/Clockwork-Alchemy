using UnityEngine;

public class ConvoyerShow : MonoBehaviour, IConveyer{
    public int getConveyerRotation(){
        return 0;
    }
    
    
    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
