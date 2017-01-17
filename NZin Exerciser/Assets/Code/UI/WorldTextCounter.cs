using UnityEngine;
using UnityEngine.UI;
using NZin.Entities;


public class WorldTextCounter : Entity {


    public delegate void UpdateInt( int value );

    public CompoundText Text                { get; set; }


    public WorldTextCounter( Entity decoratee ) 
        : base( decoratee ) {
        Text = CreateFromPrefab( decoratee.Id.ToString() );
        Text.Text = "???";
    }
    // zzz td: Do the GO destruction the way Unity wants you to. Then make it integrate with Entity/Dispsoable.
    public void Destroy( Entity e ) {
        if( Text != null )
            GameObject.Destroy( Text.gameObject );
    }
        



    private static CompoundText CreateFromPrefab( string nameSuffix ) {
        if( prefab == null ) {
            prefab = (GameObject)Resources.Load( "NZin/UI/WorldCounterPrefab" );
        }
        GameObject go = GameObject.Instantiate( prefab );
        go.transform.SetParent( UiRoot.Instance.transform );
        go.name = "__Counter_" + nameSuffix;
        return go.GetComponent<CompoundText>();
    }
    static GameObject prefab;


}