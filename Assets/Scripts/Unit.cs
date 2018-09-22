
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public enum status_effects_names{
	attack_change,
	defense_change,
	color_damage_mod, // takes less damage from color weakness - default is 1.5 multiplied by all color-resistance changes.
	reflect
}
public class status_effect{
	public status_effects_names type;
	public float amt;
	public int duration; // duration -1 means never expire
	public status_effect(status_effects_names type_, float amt_, int duration_ = -1){
		type = type_;
		amt = amt_;
		duration = duration_;
	}
}
public class PlayerStats
{
	public float maxHealth = 1f;
	public float _curHealth;
	public float attack;
	public float defense;
	public string color;
	//status effects:
	public int stun_turns = 0;
	public int root_turns = 0;
    public int power_turns = 0;
	public int disarm_turns = 0;
	public int moveRange = 5;
	public int attackRange = 1;
	public List<status_effect> effects = new List<status_effect>();
	public PlayerStats(float mh, float ch, float a, float d, int move_range, int attack_range, string c){
		maxHealth = mh;
		_curHealth = ch;
		attack = a;
		defense = d;
		color = c;
		moveRange = move_range;
		attackRange = attack_range;
	}
}


public class Unit : MonoBehaviour {
	public int x;
	public int y;
	public string unitName;
	public string team;

	public GameObject HealthBar;

	public GameObject movePositon;
	public GameObject attackBox;
	public float moveDistance;
	public Renderer playerSprite;

	public bool isChosen = false;
	public GameManager gm;
	Hashtable units = new Hashtable();

	public bool hasMoved = false;
	public bool hasAttacked = false;
	public PlayerStats ps;
	public bool belongToPlayer1 = true;
	Vector3 home;

	// for healthbar
	public GameObject newobj;

	public class OtherPlayer
	{
		int x;
		int y;
	}
	int width;
	int height;

	public void resetMoved()
	{
		hasMoved = false;
		isChosen = false;
		hasAttacked = false;
		Debug.Log("as " + unitName + " " + hasMoved);

	}

	private void Start() // the "constructor" 
	{
		home = this.transform.position;
		playerSprite = GetComponent<Renderer>();
        moveDistance = 1.0275f;// playerSprite.bounds.size.x;
		gm = (GameManager) GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		width = gm.width;
		height = gm.height;
		// health bar
		newobj = Instantiate(HealthBar) as GameObject;
		units.Clear ();
		GameObject[] units_t = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < units_t.Length; i++) {
			units.Add (i, units_t [i]);
		}

	}

	bool CheckOnOther(Hashtable units, int x, int y)
	{
		for (var i = 0; i < units.Count; i++)
		{
			var u = (GameObject)units[i];
			if (u != null) { 
				if (x == (u).transform.position.x && y == (u).transform.position.y)
					return false;
			}
		}
		return true;
	}
	bool CheckOnEnemy(Hashtable units, int x, int y)
	{
		for (var i = 0; i < units.Count; i++)
		{
			var u = (GameObject)units[i];
			if (u != null)
			{
				if (x == (u).transform.position.x && y == (u).transform.position.y && (u).GetComponent<Unit>().team == this.team)
					return false;
			}
		}
		return true;
	}

	private void OnMouseDown()
	{
		Debug.Log("mouse down" + gm.spell);
		//spell selected
		if (gm.spell != castable_spells.nothing ) {  //spell selected
			if (gm.spell == castable_spells.change_color && GameManager.myTurn (belongToPlayer1)) { // 
				ps.color = "blue";//fix later.
				Debug.Log ("color change" + ps.color);
			} else if (gm.spell != castable_spells.nothing) {  //spell selected
				if (gm.spell == castable_spells.weaken && !GameManager.myTurn (belongToPlayer1)) { // 
					ps.effects.Add (new status_effect (status_effects_names.attack_change, -5));
					Debug.Log ("perma-weakened");
				}

			} else {
				Debug.Log ("can't cast the spell here");
			}
		}
		if (!(GameManager.myTurn(belongToPlayer1))) //not your turn
		{
			Debug.Log("blocked click1");

			return;
		}
		if (!isChosen) // not chosen : deselect, your turn
		{

			GameObject[] allRanges = GameObject.FindGameObjectsWithTag ("Range");
			for (int i = 0; i < allRanges.Length; i++) {
				GameObject.Destroy(allRanges[i]);
			}
			isChosen = true;
			showRange(ps.moveRange, ps.attackRange);
		}
		else //is chosen, your turn : deselect.
		{
			isChosen = false;
			resetRangeTiles ();
		}
		gm.spell = castable_spells.nothing;
	}

	void showRange(int moveRange, int attackRange)
	{
		int range = moveRange > attackRange ? moveRange : attackRange;
		Vector3 playPos = gameObject.transform.position;
		for (int i = -1 * range; i <= range; i++)
		{
			for (int j = -1 * range; j <= range; j++)
			{
				int diff = range - Mathf.Abs(i); // taxicab distance
				Vector3 moveOption = new Vector3(Mathf.Round(playPos.x + i * moveDistance), Mathf.Round(playPos.y + j * moveDistance), Mathf.Round(playPos.z));
				if (i == 0 && j == 0) // can't move onto self
				{
					continue;
				}
				int distance = Mathf.Abs (j) + Mathf.Abs (i);
				if (distance  <= range) // within range
				{
					int tilex = x + i;
					int tiley = y + j;
					if (tilex >= 0 && tiley >= 0 && tilex < width && tiley < height && CheckOnOther(units, tilex, tiley) && distance <= moveRange) // if nothing else is there and it's on the board
					{
						GameObject child;
						//Debug.Log ("move position is : " + moveOption);
						child = Instantiate(movePositon, moveOption, Quaternion.identity) as GameObject;
						child.transform.parent = gameObject.transform;
						Tile tile = child.GetComponent(typeof(Tile)) as Tile;
						tile.x = tilex;
						tile.y = tiley;
						tile.type = Type.LAND;
						tile.gameManager = gm;
						units.Clear ();
						GameObject[] units_t = GameObject.FindGameObjectsWithTag("Player");
						for (int it= 0; it < units_t.Length;it++) {
							units.Add (it, units_t [it]);
						}
					}
					if (tilex >= 0 && tiley >= 0 && tilex < width && tiley < height && !CheckOnOther(units, tilex, tiley) && CheckOnEnemy(units, tilex, tiley)&& distance <= attackRange)
					{
						Debug.Log("Attacks debug");
						GameObject child;
						//hasAttacked = true;
						child = Instantiate(attackBox, moveOption, Quaternion.identity) as GameObject;
						child.transform.parent = gameObject.transform;
						Tile tile = child.GetComponent(typeof(Tile)) as Tile;
						tile.x = tilex;
						tile.y = tiley;
						tile.gameManager = gm;
						units.Clear ();
						GameObject[] units_t = GameObject.FindGameObjectsWithTag("Player");
						for (int it= 0; it < units_t.Length;it++) {
							units.Add (it, units_t [it]);
						}
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (newobj);
		newobj.GetComponentInChildren<BarScript>().damage = this.ps._curHealth/this.ps.maxHealth;
		newobj.GetComponentInChildren<BarScript>().player = this.transform.position;
	}
	public void performAction()
	{
		//Debug.Log("Performing " + unitName);
	}

	public void resetRangeTiles() {

		foreach (Transform child in transform)
		{
            if (child.gameObject.tag != "Particle" && child.gameObject.tag != "StunParticle")
                GameObject.Destroy(child.gameObject);
		}
	}

}
