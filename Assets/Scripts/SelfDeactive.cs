using UnityEngine;
using System.Collections;

public class SelfDeactive : MonoBehaviour {

    public float sec = 3f;
	void Update () 
	{
        StartCoroutine(selfDeactiveAfterSec());
	}
	
    IEnumerator selfDeactiveAfterSec()
    {
        yield return new WaitForSeconds(sec);

        gameObject.SetActive(false);
    }
}
