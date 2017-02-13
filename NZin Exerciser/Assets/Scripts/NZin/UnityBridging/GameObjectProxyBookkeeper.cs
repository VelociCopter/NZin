using System;
using System.Collections.Generic;
using UnityEngine;


namespace NZin {

/// <summary>
/// This class helps sort out all of the simultaneous input events that disparate GameObjectProxies may receive.
/// One such use is to create UI elements (that are also GameObjectProxies) that consume click events
///     so they don't bleed through to in-world Proxies.
/// </summary>
public class GameObjectProxyBookkeeper : Monoton<GameObjectProxyBookkeeper> {
    public const bool DEBUG_LOG = false;
    #pragma warning disable 429, 162

    // Used by the inspector to make a log of what happened each frame
    public bool MakeReports = false;
    public string Report        { get; private set; }



    public void Poked( GameObjectProxy proxy ) {
        poked.Add( proxy );
    }
    public void Released( GameObjectProxy proxy ) {
        released.Add( proxy );
    }
    public void Scraped( GameObjectProxy proxy ) {
        scraped.Add( proxy );
    }

    void LateUpdate() {
        var tapPositionNow = Input.mousePosition;

        /* NOTES:
         *  Currently there assumes only 1 touch per frame. Need some more logic to handle multi-touch.
         */
        if( DEBUG_LOG || MakeReports ) {
            Report = string.Format( "Proxy Bookkeeper LateUpdate: Poked:{0}, Released:{1}, Scraped:{2}",
                poked.Count, released.Count, scraped.Count
            );
        }
        bool worthLogging = poked.Count + released.Count + scraped.Count > 0;

        poked.Sort();
        foreach( var proxy in poked ) {
            if( proxy.Receiver != null ) {
                if( MakeReports ) {
                    Report += string.Format( "\nSending {0} Poke to {1}",
                        proxy.OccludesInput? "occluding":"piercing", proxy.Receiver 
                    );
                }

                proxy.Receiver.TouchDown( proxy.TapStartedAt.Value );

                // Stop further processing if this Proxy should "block" any input from going through it into Proxies that are behind it
                if( proxy.OccludesInput )
                    break;
            }
        }

        released.Sort();
        foreach( var proxy in released ) {
            if( proxy.Receiver != null ) {
                if( MakeReports ) {
                    Report += string.Format( "\nSending {0} Release to {1}",
                        proxy.OccludesInput? "occluding":"piercing", proxy.Receiver 
                    );
                }

                proxy.Receiver.TouchUp( tapPositionNow );
                if( proxy.OccludesInput )
                    break;
            }
        }

        scraped.Sort();
        foreach( var proxy in scraped ) {
            if( proxy.Receiver != null ) {
                if( MakeReports ) {
                    Report += string.Format( "\nSending {0} Scrape to {1}",
                        proxy.OccludesInput? "occluding":"piercing", proxy.Receiver 
                    );
                }

                proxy.Receiver.TouchDrag( proxy.TapStartedAt.Value, tapPositionNow );
                if( proxy.OccludesInput )
                    break;
            }
        }

        if( DEBUG_LOG && worthLogging ) {
            Debug.Log( Report );
        }

        poked.Clear();
        released.Clear();
        scraped.Clear();
    }


    List<GameObjectProxy> poked = new List<GameObjectProxy>();
    List<GameObjectProxy> released = new List<GameObjectProxy>();
    List<GameObjectProxy> scraped = new List<GameObjectProxy>();
}

}