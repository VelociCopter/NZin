using UnityEngine;



public interface NamedPrefab {
    string PrefabName   { get; }
}

public abstract class PrefabTemplate<T,U> : MonoBehaviour where T : MonoBehaviour where U : NamedPrefab, new() {

    public static T Instantiate( MonoBehaviour parent ) {
        return Instantiate( parent.transform );
    }
    public static T Instantiate( Transform parent ) {
        var i = Instantiate();
        i.transform.SetParent( parent, false );
        return i;
    }
    public static T Instantiate() {
        if( prefabInstance == null ) {
            U p = new U();
            prefabInstance = Resources.Load<GameObject>( p.PrefabName );
        }
        var go = GameObject.Instantiate( prefabInstance );
        var rv = go.GetComponent<T>();
        if( rv == null )
            rv = go.AddComponent<T>();

        return rv;
    }

    static GameObject prefabInstance;
}
