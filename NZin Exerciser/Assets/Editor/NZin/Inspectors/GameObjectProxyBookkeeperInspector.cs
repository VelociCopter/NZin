using UnityEngine;
using UnityEditor;
using System.Collections;
using NZin;


[ CustomEditor( typeof( GameObjectProxyBookkeeper ))]
public class GameObjectProxyBookkeeperInspector : Editor {


    public override void OnInspectorGUI() {

        EditorUtility.SetDirty( target );
        GameObjectProxyBookkeeper keeper = (GameObjectProxyBookkeeper)target;

        keeper.MakeReports = true;
        EditorGUILayout.LabelField( keeper.Report );

    }

}