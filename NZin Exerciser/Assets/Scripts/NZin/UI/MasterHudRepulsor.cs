using System.Collections.Generic;
using UnityEngine;
using NZin;


namespace NZin {

// TODO: This class will almost certainly need to implement a collision structure to handle lots of Repulsors. For now, it can really only handle a few.
// zzz rename to be consistent w/ new UiRepulsor or whatever I end up with
public class MasterHudRepulsor : Monoton<MasterHudRepulsor> {


    public void Register( UiRepulsor repulsor ) {
        repulsors.Add( repulsor );
    }
    public void Deregsiter( UiRepulsor repulsor ) {
        repulsors.Remove( repulsor );
    }


    void LateUpdate() {
        // TODO: Less bad collision detection. Right now it's about as bad as it can be!
        //  1) Don't check each repulsor against each other repulsor
        //  2) Aggregate corrective vectors rather than just the first one encountered
        //  3) Rely on Unity Colliders or write a collision structure
        UiRepulsor luckyRepulsor = null;
        foreach( var repulsor in repulsors ) {
            if( luckyRepulsor == null )
                luckyRepulsor = repulsor;
            if( repulsor == luckyRepulsor)
                continue;
            
            if( luckyRepulsor.CollidesWith( repulsor )) {
                Vector3 toLucky = luckyRepulsor.ScreenPosition - repulsor.ScreenPosition;
                toLucky.z = 0f;
                toLucky.Normalize();
                toLucky *= 100f;
//                Debug.Log( "Collision. toLucky="+toLucky );
                repulsor.AddRepellantScreenVector( -toLucky );
                luckyRepulsor.AddRepellantScreenVector( toLucky );
            } else {
                luckyRepulsor = repulsor;
            }
        }
    }


    List<UiRepulsor> repulsors = new List<UiRepulsor>();
}

}