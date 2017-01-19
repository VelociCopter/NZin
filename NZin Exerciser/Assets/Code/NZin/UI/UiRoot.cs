using UnityEngine;


public class UiRoot : MonoBehaviour {


    public static UiRoot Instance { get { 
            if( _instance == null ) {
                Debug.LogError( "UiRoot must first be initialized." );
            }
            return _instance; 
        } 
    }


    void Start() {
        _instance = this;
    }
    private static UiRoot _instance;


}