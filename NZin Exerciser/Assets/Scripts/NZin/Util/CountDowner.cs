using UnityEngine;

namespace NZin {



/// <summary>
/// A bresenham-style counter
/// </summary>
public class CountDowner {


    public float Period             { get; set; }
    public bool Paused              { get; set; }
    public bool IsReady             { get { return value <= 0; } }
    /// <summary>
    /// Returns the progress normalized to [0,1]
    /// </summary>
    /// <returns></returns>
    public float RelativeProgress   { get {
            return 1-( Mathf.Clamp01( value / Period ));
        } 
    }
    public float RemainingTime      { get {
            return Mathf.Max( 0f, value );
        }
    }



    public CountDowner( float period = 1f ) {
        Period = period;
        Paused = false;
        value = period;
    }



    /// <summary>
    /// Moves the timer forward by the specified amount. NOTE: If this timer is paused, this call is a no-op
    /// </summary>
    /// <param name="seconds">Seconds.</param>
    public void CountDown( float seconds ) {
        if( Paused ) return;

        value -= seconds;
    }


    /// <summary>
    /// Consumes one "step." 
    /// You might want this in cases where a Value can overflow a period, or Values can accumulated post-period
    /// </summary>
    public void Consume() {
        value += Period;
    }


    public void Reset() {
        value = Period;
    }
    public void Reset( float newPeriod ) {
        Period = newPeriod;
        Reset();
    }


    float value = 0f;
}

}