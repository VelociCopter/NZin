using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NZin;


[RequireComponent( typeof(UnityEngine.UI.Text ))]
public class CompoundText : MonoBehaviour {

	
    // To be set in the inspector
    public CompoundText DaisyChain;
   
    public delegate void TextUpdate( string text );
    public event TextUpdate TextChanged;

    /// <summary>
    /// Assign an entity to this value and the CompoundText custom UnityEditor-Inspector will try to populate it
    /// </summary>
    public Entity InspectorLink;


    void Awake() {
        primaryTextBox = this.gameObject.GetComponent<Text>();
    }


    private string _text;
    public string Text {
        set {
            _text = value;

            if( primaryTextBox != null )
                primaryTextBox.text = _text;
            
            if( DaisyChain != null ) {
                DaisyChain.Text = _text;
            }

            if( TextChanged != null ) {
                TextChanged( _text );
            }
        }
        get {
            return _text;
        }
    }


    Text primaryTextBox;
}
