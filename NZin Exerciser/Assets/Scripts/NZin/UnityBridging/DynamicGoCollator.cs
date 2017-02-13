using UnityEngine;


public class DynamicGoCollator : Monoton<DynamicGoCollator> {
    void Start() {
        name = "__root";
    }


    public void Take( GameObject go ) {
        go.transform.SetParent( this.transform, true );
    }
}