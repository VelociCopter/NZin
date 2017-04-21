using UnityEngine;
using UnityEditor;
using System.Collections;
using NZin;


[ CustomEditor( typeof( CompoundText ))]
public class CompoundTextInspector : Editor {


    public override void OnInspectorGUI() {

        EditorUtility.SetDirty( target );
        CompoundText compoundText = (CompoundText)target;

        var entity = compoundText.DeferInspection;
        if( entity != null ) {
            EditorGUILayout.LabelField( 
                string.Format( "Model: e_{0}", entity.Id )
            );

            EditorGUILayout.LabelField( string.Format( "Disposed? {0}", entity.IsDisposed ));
        }
    }

}