using UnityEngine;
using UnityEditor;
using System.Collections;
using NZin.Entities;


[ CustomEditor( typeof( CompoundText ))]
public class CompoundTextInspector : Editor {


    public override void OnInspectorGUI() {

        EditorUtility.SetDirty( target );
        CompoundText compoundText = (CompoundText)target;

        var entity = compoundText.InspectorLink;
        if( entity != null ) {
            EditorGUILayout.LabelField( 
                string.Format( "Model: e_{0}", entity.Id )
            );

            Disposable disposable;
            if( entity.HasDecoration<Disposable>( out disposable )) {
                EditorGUILayout.LabelField( string.Format( "Disposed? {0}", disposable.IsDisposed ));
            }
        }
    }

}