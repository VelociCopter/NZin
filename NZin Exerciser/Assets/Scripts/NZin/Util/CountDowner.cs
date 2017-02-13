using UnityEngine;


/// <summary>
/// A bresenham-style counter
/// </summary>
public class CountDowner {


    public float Period             { get; set; }
    public bool Paused              { get; set; }
    public bool IsReady             { get { return value <= 0; } }
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