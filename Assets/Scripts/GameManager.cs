using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public enum castable_spells{
	nothing,
	change_color,
	weaken
}
public class GameManager : MonoBehaviour {

    Sprite[] spritesheet;

    public List<List<Tile>> map = new List<List<Tile>>();
	public GameObject tilePrefab;
	public int width = 10;
	public int height = 10;
	public GameObject unitPrefab1;
	public GameObject unitPrefab2;
	public static bool isPlayer1Turn = true;
	public int curUnitIndex = 0;
	public Text text_1;
	public Text text_2;
	public int player_1_mana=200;
	public int player_2_mana=200;

    string CSu1;
    string CSu2;
    string CSu3;
    string CSu4;
    string CSu5;
    string CSu6;


	public castable_spells spell = castable_spells.nothing;
	Camera camera;
	// keep track of team1's players
	public List<GameObject> t1players = new List<GameObject>();
	// keep track of team2's players
	public List<GameObject> t2players = new List<GameObject>();
    public GameObject powerUpPrefab;
    public GameObject stunPrefab;
    public GameObject deathPrefab;
    public GameObject currentActingPlayer;



    // Use this for initialization
    void Start () {
        spritesheet = Resources.LoadAll<Sprite>("SpriteMaster");

        CSu1 = PlayerPrefs.GetString("CharacterSelectu1");
        Debug.Log("value of CharacterSelectu1 is " + CSu1);
        Debug.Log("Lets see if it has key" + PlayerPrefs.HasKey("CharacterSelectu1"));

        CSu2 = PlayerPrefs.GetString("CharacterSelectu2");
        Debug.Log("value of CharacterSelectu2 is " + CSu2);
        Debug.Log("Lets see if it has key" + PlayerPrefs.HasKey("CharacterSelectu2"));

        CSu3 = PlayerPrefs.GetString("CharacterSelectu3");
        Debug.Log("value of CharacterSelectu3 is " + CSu3);
        Debug.Log("Lets see if it has key" + PlayerPrefs.HasKey("CharacterSelectu3"));

        CSu4 = PlayerPrefs.GetString("CharacterSelect2u4");
        Debug.Log("value of CharacterSelectu4 is " + CSu4);
        Debug.Log("Lets see if it has key" + PlayerPrefs.HasKey("CharacterSelect2u4"));

        CSu5 = PlayerPrefs.GetString("CharacterSelect2u5");
        Debug.Log("value of CharacterSelectu5 is " + CSu5);
        Debug.Log("Lets see if it has key" + PlayerPrefs.HasKey("CharacterSelect2u5"));

        CSu6 = PlayerPrefs.GetString("CharacterSelect2u6");
        Debug.Log("value of CharacterSelectu6 is " + CSu6);
        Debug.Log("Lets see if it has key" + PlayerPrefs.HasKey("CharacterSelect2u6"));

        CreateMap ();
		SetUpPlayers ();
		text_1.text = "Player 1 Mana : " + player_1_mana.ToString();
		//update mana texts.
		text_2.text = "Player 2 Mana : " + player_2_mana.ToString();
	}
	// Update is called once per frame
	void Update () {
		//    turnAction();
		if (Input.GetMouseButtonDown (0)) {
			Vector3 c = Input.mousePosition;
			c.z = 10f;
			//	Debug.Log(Camera.main.ScreenToWorldPoint(c)); this actually works

		}
	}
    
	void CreateMap() {
		text_1.text = "Player 1 Mana = " + player_1_mana.ToString ();
		text_2.text = "Player 2 Mana = " + player_2_mana.ToString ();
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				GameObject tileIn = Instantiate(tilePrefab, new Vector3(i, j, gameObject.transform.position.z), Quaternion.identity) as GameObject;

				Tile tile = tileIn.GetComponent(typeof(Tile)) as Tile;
				tile.x = i;
				tile.y = j;
				tile.type = Type.LAND;
				tile.gameManager = this;
				tileIn.transform.parent = gameObject.transform;


				//Debug.Log(i + " " + j);
			}
		}
	}

	void SetUpPlayers() {

		// TEAM 1
		// first player
		// replace with something good - such as a loop and not separate variables for each character. Maybe a list or a hash table.
		GameObject t1unit1 = Instantiate(unitPrefab1, new Vector3(0, 0, 0), Quaternion.identity);
		t1unit1.GetComponent<Unit>().unitName = "A";
		t1unit1.GetComponent<Unit>().x = 0;
		t1unit1.GetComponent<Unit>().y = 0;
        t1unit1.GetComponent<Unit>().belongToPlayer1 = true;
        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals(CSu1))
            {
                t1unit1.GetComponent<SpriteRenderer>().sprite = S;
            }
        }
        //hard code for now, TODO: stop hard-coding it
        t1unit1.GetComponent<Unit> ().ps = new PlayerStats (10, 10, 10, 2, 5, 1, "red");
        t1players.Add (t1unit1);


        t1unit1 = Instantiate(unitPrefab1, new Vector3(1, 0, 0), Quaternion.identity);
        t1unit1.GetComponent<Unit>().x = 1;
        t1unit1.GetComponent<Unit>().y = 0;
        t1unit1.GetComponent<Unit>().unitName = "B";
        t1unit1.GetComponent<Unit>().belongToPlayer1 = true;
        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals(CSu2))
            {
                t1unit1.GetComponent<SpriteRenderer>().sprite = S;
            }
        }
        //hard code for now, TODO: stop hard-coding it
        t1unit1.GetComponent<Unit> ().ps = new PlayerStats (10, 10, 10, 2, 5, 1, "red");
        t1players.Add(t1unit1);


        t1unit1 = Instantiate(unitPrefab1, new Vector3(2, 0, 0), Quaternion.identity);
        t1unit1.GetComponent<Unit>().x = 2;
        t1unit1.GetComponent<Unit>().y = 0;
        t1unit1.GetComponent<Unit>().unitName = "C";
        t1unit1.GetComponent<Unit>().belongToPlayer1 = true;

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals(CSu3))
            {
                t1unit1.GetComponent<SpriteRenderer>().sprite = S;
            }
        }

        //hard code for now, TODO: stop hard-coding it
        t1unit1.GetComponent<Unit> ().ps = new PlayerStats (10, 10, 10, 2, 5, 1, "red");
        t1players.Add(t1unit1);


		// TEAM 2
		// second player
		GameObject t2unit1 = Instantiate(unitPrefab2, new Vector3(8, 8, 0), Quaternion.identity);
        t2unit1.GetComponent<Unit>().x = 8;
        t2unit1.GetComponent<Unit>().y = 8;
        t2unit1.GetComponent<Unit>().unitName = "D";
        t2unit1.GetComponent<Unit>().belongToPlayer1 = false;
        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals(CSu4))
            {
                t2unit1.GetComponent<SpriteRenderer>().sprite = S;
            }
        }
        //hard code for now, TODO: stop hard-coding it
        t2unit1.GetComponent<Unit> ().ps = new PlayerStats (10, 10, 10, 2, 5, 1, "red");
        t2players.Add (t2unit1);


		t2unit1 = Instantiate(unitPrefab2, new Vector3(7, 8, 0), Quaternion.identity);
		t2unit1.GetComponent<Unit>().x = 7;
		t2unit1.GetComponent<Unit>().y = 8;
		t2unit1.GetComponent<Unit>().unitName = "E";
		t2unit1.GetComponent<Unit> ().belongToPlayer1 = false;

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals(CSu5))
            {
                t2unit1.GetComponent<SpriteRenderer>().sprite = S;
            }
        }
        //hard code for now, TODO: stop hard-coding it
        t2unit1.GetComponent<Unit> ().ps = new PlayerStats (10, 10, 10, 2, 5, 1, "red");
        t2players.Add(t2unit1);

        t2unit1 = Instantiate(unitPrefab2, new Vector3(6, 8, 0), Quaternion.identity);
        t2unit1.GetComponent<Unit>().x = 6;
        t2unit1.GetComponent<Unit>().y = 8;
        t2unit1.GetComponent<Unit>().unitName = "F";
        t2unit1.GetComponent<Unit>().belongToPlayer1 = false;
        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals(CSu6))
            {
                t2unit1.GetComponent<SpriteRenderer>().sprite = S;
            }
        }
        //hard code for now, TODO: stop hard-coding it
        t2unit1.GetComponent<Unit> ().ps = new PlayerStats (10, 10, 10, 2, 5, 1, "red");
        t2players.Add(t2unit1);

		// third player
		// fourth player

		//mana
		player_1_mana = 200;
		player_2_mana = 200;
	}

	public void MoveSelectedUnitTo(int x, int y, GameObject unit) {
		unit.GetComponent<Unit> ().x = x;
		unit.GetComponent<Unit> ().y = y;
		unit.transform.position = new Vector3 (x, y, 0);
	}

	public void turnAction()
	{
		int numberOfUnits;
		if (isPlayer1Turn)
		{
			//    t1players.ElementAt(curUnitIndex).GetComponent<Unit> ().x;
			numberOfUnits = t1players.Count;
			var unit = t1players.ElementAt(curUnitIndex);
			unit.GetComponent<Unit>().performAction();
		}
		else
		{
			numberOfUnits = t2players.Count;
			var unit = t2players.ElementAt(curUnitIndex);
			unit.GetComponent<Unit>().performAction();
		}
		curUnitIndex++;
		if (curUnitIndex >= numberOfUnits)
		{
			//    isPlayer1Turn = !isPlayer1Turn;
			curUnitIndex = 0;
		}
	}

	public static bool myTurn(bool belongsPlayer1)
	{
		if (belongsPlayer1 && isPlayer1Turn)
		{

			return true;
		}

		if (!belongsPlayer1 && !isPlayer1Turn)
		{

			return true;
		}

		return false;
	}

	public void nextTurn()
	{
		decrementStatusEffects (isPlayer1Turn);
		isPlayer1Turn = !isPlayer1Turn;

		for (var i = 0; i < t1players.Count; i++)
		{
			if (t1players.ElementAt(i) != null)
			{

				t1players.ElementAt(i).GetComponent<Unit>().resetMoved();
				t1players.ElementAt (i).GetComponent<Unit> ().resetRangeTiles ();
			}
		}
		Debug.Log("");
		for (var i = 0; i < t2players.Count; i++)
		{
			if (t2players.ElementAt(i) != null)
			{
				t2players.ElementAt(i).GetComponent<Unit>().resetMoved();
				t2players.ElementAt (i).GetComponent<Unit> ().resetRangeTiles ();
			}
		}



	}


	public void fakeAttack(GameObject attacker, GameObject target) {
		GameObject.Destroy (target);
	}

	public void buttooonsPressings()
	{
		nextTurn();
	}

	// function to decrement the count of the status effects
	public void decrementStatusEffects(bool p1turn) {
		// if p1's turn
		List<GameObject> units;
		if (p1turn) {
			units = t1players;
		} else {
			units = t2players;
		}
		// iterate over all units in t1, and reduce stats
		for (int i = 0; i < units.Count; i++) {
			Unit unit = units [i].GetComponent<Unit> ();
            if (unit.ps.root_turns == 0)
            {
                foreach (Transform child in units[i].transform)
                {
                    if (child.tag == "StunParticle")
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                }
            }
            if (unit.ps.power_turns == 0)
            {
                foreach (Transform child in units[i].transform)
                {
                    if (child.tag == "Particle")
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                }
            }
            Debug.Log("Empower turns: " + unit.ps.power_turns);
            if (unit.ps.stun_turns > 0)
				unit.ps.stun_turns = unit.ps.stun_turns - 1;
			if (unit.ps.root_turns > 0)
				unit.ps.root_turns = unit.ps.root_turns - 1;
			if (unit.ps.disarm_turns > 0)
				unit.ps.disarm_turns = unit.ps.disarm_turns - 1;
            if (unit.ps.power_turns > 0)
                unit.ps.power_turns = unit.ps.power_turns - 1;
            //iterate and reduce the turns for each status effect
            List<int> to_remove = new List<int>(); // must be sorted lowest to highest
			for (int j = 0; j < unit.ps.effects.Count; j++) {
				unit.ps.effects [j].duration -= 1;
				if (unit.ps.effects [j].duration == 0) {
					to_remove.Add (j);
				}
			}
			for (int j = to_remove.Count - 1; j >= 0; j--) { // must go backwards
				unit.ps.effects.RemoveAt(to_remove[j]); 
			}
			//			Debug.Log ("unit's ps stun turns: " + unit.ps.stun_turns);
			//			Debug.Log ("unit's ps root turns: " + unit.ps.root_turns);
			//			Debug.Log ("unit's ps disarm turns: " + unit.ps.disarm_turns);
		}
	}

	public void safelyDestroyUnits(GameObject unit) {
		//		Debug.Log ("inside the safely destroyed unit" + unit.name);
		Destroy(unit.GetComponent<Unit>().newobj);
		if (t1players.Contains(unit)) {
			//			Debug.Log("found the unit and removing from list1");
			t1players.Remove (unit);
		}
		else if (t2players.Contains(unit)) {
			//			Debug.Log("found the unit and removing from list2");
			t2players.Remove (unit);
		}
        GameObject stunParticle = Instantiate(deathPrefab,unit.transform.position, Quaternion.identity);
        Destroy (unit);
	}
	//spells

	public void global_empower() // empowers your entire team
	{
		if(isPlayer1Turn && player_1_mana >= 50){
			for(int i=0; i<t1players.Count;i++){
				t1players[i].GetComponent<Unit>().ps.effects.Add(new status_effect(status_effects_names.attack_change ,10f,3));
                t1players[i].GetComponent<Unit>().ps.power_turns = 3;
                GameObject powerParticle = Instantiate(powerUpPrefab, t1players[i].transform.position, Quaternion.identity) as GameObject;
                powerParticle.transform.parent = t1players[i].transform;
            }
			player_1_mana -= 50;
		}
		if(!isPlayer1Turn&& player_2_mana >= 50){
			for(int i=0; i<t2players.Count;i++){
				t2players[i].GetComponent<Unit>().ps.effects.Add(new status_effect(status_effects_names.attack_change ,10f,3));
                t2players[i].GetComponent<Unit>().ps.power_turns = 3;
                GameObject powerParticle = Instantiate(powerUpPrefab, t2players[i].transform.position, Quaternion.identity) as GameObject;
                powerParticle.transform.parent = t2players[i].transform;
            }
			player_2_mana -= 50;
		}
		Debug.Log ("empowered" + player_1_mana + " " + player_2_mana);
		//update mana texts.
		text_1.text = "Player 1 Mana : " + player_1_mana.ToString();
		text_2.text = "Player 2 Mana : " + player_2_mana.ToString();
	}
	public void global_stun() // stuns the entire enemy team
	{
		if(isPlayer1Turn && player_1_mana >= 50){
			for(int i=0; i<t2players.Count;i++){
				t2players [i].GetComponent<Unit>().ps.stun_turns = 2;
                GameObject stunParticle = Instantiate(stunPrefab, t2players[i].transform.position, Quaternion.identity);
                stunParticle.transform.parent = t2players[i].transform;
            }
			player_1_mana -= 50;
		}
		if(!isPlayer1Turn&& player_2_mana >= 50){
			for(int i=0; i<t1players.Count;i++){
				t1players [i].GetComponent<Unit>().ps.stun_turns = 2;
                GameObject stunParticle = Instantiate(stunPrefab, t1players[i].transform.position, Quaternion.identity);
                stunParticle.transform.parent = t1players[i].transform;
            }
			player_2_mana -= 50;
		}
		Debug.Log ("stun all" + player_1_mana + " " + player_2_mana);
		//update mana texts.
		text_1.text = "Player 1 Mana : " + player_1_mana.ToString();
		text_2.text = "Player 2 Mana : " + player_2_mana.ToString();
	}
	public void change_color() // ally team targeted spell
	{
		if (spell != castable_spells.nothing) { //spell already selected
			Debug.Log ("a spell is already selected!");
		} else {
			int mana_cost = 10;
			if (isPlayer1Turn && player_1_mana >= mana_cost) {
				spell = castable_spells.change_color;
				player_1_mana -= mana_cost;
			}
			if (!isPlayer1Turn && player_2_mana >= mana_cost) {
				spell = castable_spells.change_color;
				player_2_mana -= mana_cost;
			}
			Debug.Log ("change color selected" + player_1_mana + " " + player_2_mana);
			//update mana texts.
			text_1.text = "Player 1 Mana : " + player_1_mana.ToString ();
			text_2.text = "Player 2 Mana : " + player_2_mana.ToString ();
		}
	}
	public void perma_weaken() // enemy targeted spell
	{
		if (spell != castable_spells.nothing) { //spell already selected
			Debug.Log ("a spell is already selected!");
		} else {

			int mana_cost = 100;
			if (isPlayer1Turn && player_1_mana >= mana_cost) {
				spell = castable_spells.weaken;
				player_1_mana -= mana_cost;
			}
			if (!isPlayer1Turn && player_2_mana >= mana_cost) {
				spell = castable_spells.weaken;
				player_2_mana -= mana_cost;
			}
			Debug.Log ("weaken selected" + player_1_mana + " " + player_2_mana);
			//update mana texts.
			text_1.text = "Player 1 Mana : " + player_1_mana.ToString ();
			text_2.text = "Player 2 Mana : " + player_2_mana.ToString ();
		}
	}
}