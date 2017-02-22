using UnityEngine;

namespace NZin {



public class Monoton<T> : MonoBehaviour where T : MonoBehaviour {


    public delegate void UpdateEvent( float seconds );
    public UpdateEvent Updated;



	public static T Instance { get {
            if( instance == null ) {
                instance = FindMe();
                if( !instance )
        			Instantiate();
            }
		return instance;
	} }



    void Start() {
        // Highlander
        var existingInstance = FindMe();
        if( existingInstance != this ) {
            Debug.LogWarning( string.Format( "Found another Monoton like this one ({0}). Killing this one.",
                DynamicName() 
            ));
            GameObject.DestroyImmediate( this.gameObject );
        } else {
            DontDestroyOnLoad( this.gameObject );
        }
    }



    static T FindMe() {
        return GameObject.FindObjectOfType<T>();
    }

	static void Instantiate() {
        GameObject go = new GameObject();
        go.name = DynamicName();
		instance = go.AddComponent<T>();
	}

    static string DynamicName() {
        return "__"+typeof(T).ToString()+"_mt_";
    }



    protected virtual void Initialize() { }
    protected virtual void Update() { 
        if( Updated != null )
            Updated( Time.deltaTime );
    }


	
	static T instance;
}

}