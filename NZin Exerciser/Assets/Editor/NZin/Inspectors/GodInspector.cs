using UnityEngine;
using UnityEditor;
using System.Collections;
using NZin;


[ CustomEditor( typeof( God ))]
public class GodInspector : Editor {


    public override void OnInspectorGUI() {

        EditorUtility.SetDirty( target );
        God god = (God)target;
        var glob = ModeManager.Instance.ModeData<Glob>();

        EditorGUILayout.LabelField( string.Format( "Glob: {0}", glob ));

    }

}