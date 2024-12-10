using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager{
    public static GameManager instance;
    public void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void Start(){
        Initialize();
    }
    
    public void Initialize(){
        //initialize all childs
        ConvoyerManager.instance.Initialize();
    }

}
