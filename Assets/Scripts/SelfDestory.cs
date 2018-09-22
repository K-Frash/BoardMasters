using UnityEngine;

public class SelfDestory : MonoBehaviour {

    public float lifetime = 2f;

	void Start () 
	{
        Destroy(gameObject, lifetime);
    }
	
    
	

}
