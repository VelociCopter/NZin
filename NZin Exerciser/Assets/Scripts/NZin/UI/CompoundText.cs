using UnityEngine;
using UnityEngine.UI;



namespace NZin {


/// <summary>
/// Use this for shadowing, outlining, or other text effects
/// </summary>
[RequireComponent( typeof(UnityEngine.UI.Text ))]
public class CompoundText : MonoBehaviour {

	
    #region Inspector

    /// <summary>
    /// The DaisyChained CompoundText will update its text value every time the original one does
    /// </summary>
    public CompoundText DaisyChain;

    /// <summary>
    /// Assign an entity to this value and the CompoundText custom UnityEditor-Inspector will try to populate it
    /// In other words, when selecting this CompoundText element at runtime, the custom Inspector will show the Entity, not this UI Element
    /// </summary>
    public Entity DeferInspection;

    #endregion


   
    public delegate void TextUpdate( string text );

    public event TextUpdate TextChanged;



    void Awake() {
        primaryTextBox = this.gameObject.GetComponent<Text>();
    }



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
    private string _text;



    Text primaryTextBox;
}



}