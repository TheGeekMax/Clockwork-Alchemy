using UnityEngine;

public class ConvoyerManager : MonoBehaviour, IManager{
    public static ConvoyerManager instance;
    public void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void Initialize(){
        Debug.Log("ConvoyerManager initialized");
    }

}
