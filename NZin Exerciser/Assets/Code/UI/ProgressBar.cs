using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace NZin {
public class ProgressBar : MonoBehaviour {

    // Inspector {
    public Image Filling;
    public RectTransform Backing;
    // } Inspector


    public Color FillColor {
        set {
            Filling.color = value;
        }
    }


    public float Value {
        get {
            return v;
        }
        set {
            v = value;
            ResizeGraphics();
        }
    }
    float v;


    public float Width {
        set {
            const float HACK_HEIGHT = 10f;
            Backing.sizeDelta = new Vector2( value, HACK_HEIGHT );
            Backing.localPosition = Vector3.zero;
        }
    }


    void ResizeGraphics() {
        var full = Backing.rect.width;
        var part = v * full;
        Filling.rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, part );
    }
}
}