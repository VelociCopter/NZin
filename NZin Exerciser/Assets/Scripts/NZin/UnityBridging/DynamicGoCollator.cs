using UnityEngine;

namespace NZin {



/// <summary>
/// Call this.Take( go ) to organize the hierarchy of all of the dynamic GameObjects at runtime
/// </summary>
public class DynamicGoCollator : Monoton<DynamicGoCollator> {
    void Start() {
        name = "__root";
    }


    public void Take( GameObject go ) {
        go.transform.SetParent( this.transform, true );
    }
}

}