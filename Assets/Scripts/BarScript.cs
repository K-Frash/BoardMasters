using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    public GameObject Player;

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

    public RectTransform rectTransformPlayer;
    public Vector3 player;
    public Vector3 pos = new Vector3(200, 120, 0);
    public float damage;

	// Use this for initialization
	void Start () {
        rectTransformPlayer = GetComponent<RectTransform>();
        
        
    }
	
	// Update is called once per frame
	void Update () {
        player.z = 1;
        //Debug.Log("x:" + player.x + "y:" + player.y);
//        rectTransformPlayer.anchoredPosition = pos;
        HandleBar(damage);

		var wantedPos = Camera.main.WorldToScreenPoint (player);
//		transform.position = wantedPos;
		rectTransformPlayer.transform.position = new Vector3(wantedPos.x, wantedPos.y - 16f, wantedPos.z);
	}

    public void HandleBar(float current_health)
    {
        content.fillAmount = current_health;
    }
} 
