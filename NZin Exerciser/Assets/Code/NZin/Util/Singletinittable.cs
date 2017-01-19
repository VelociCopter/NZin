using UnityEngine;
using System.Collections;


public class Singletinittable<T> where T : Initializable, new() {


	public static T Instance {
		get {
			if( instance == null ) {
				instance = new T();
				instance.Initialize();
			}
			return instance;
		}
	}
	
	
	static T instance;
}


public interface Initializable {
    void Initialize();
    bool IsInitialized { get; }
}
