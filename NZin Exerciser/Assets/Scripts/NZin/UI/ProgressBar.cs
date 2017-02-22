using UnityEngine;
using UnityEngine.UI;

namespace NZin {



public class ProgressBar : MonoBehaviour {

    #region Inspector
    public Image Filling;
    public RectTransform Backing;
    #endregion



    public Color FillColor {
        set {
            Filling.color = value;
        }
    }


    public float Value {
        get {
            return _v;
        }
        set {
            _v = value;
            ResizeGraphics();
        }
    }
    float _v;


    public float Width {
        set {
            const float HACK_HEIGHT = 10f;
            Backing.sizeDelta = new Vector2( value, HACK_HEIGHT );
            Backing.localPosition = Vector3.zero;
        }
    }



    void ResizeGraphics() {
        var full = Backing.rect.width;
        var part = Value * full;
        Filling.rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, part );
    }
}

}