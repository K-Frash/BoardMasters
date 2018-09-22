using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class combat_result{
	public float x_damage; // damage the first player took
	public float y_damage; // damage the second player took
	List<status_effect>x_effects; // status effects inflicted on first player. Maybe we might want to add this
	List<status_effect> y_effects; // status effects inflicted on second player.
	public combat_result(float xd, float yd, List<status_effect> xe = null, List<status_effect> ye = null){
		//make it empty if it's empty
		x_damage = xd;
		y_damage = yd;
		x_effects = xe ==null ? new List<status_effect>() : xe ;
		y_effects = ye ==null ? new List<status_effect>() : ye;
	}
}
public enum TileType{
	normal,
	move,
	attack
}
public enum Type {
	LAND,
	FOREST,
	IMPASSABLE
}

public class Tile : MonoBehaviour {
	public int x;
	public int y;
	public Type type;
	public GameManager gameManager;
	public TileType tile_type = TileType.normal; 

	public Tile (int x, int y, Type type) {
		this.x = x;
		this.y = y;
		this.type = type;
    }

	void start() {
	}
	public combat_result combat(PlayerStats x, PlayerStats y){ // x attacks y
		float attack = x.attack;
		float defense = y.defense;
		// example of a status effect
		float color_weakness = 1.5f;
		float reflected_damage = 0f;
		for(int i=0;i<x.effects.Count ;i++){
			if(x.effects[i].type == status_effects_names.attack_change){
				attack = attack + x.effects[i].amt;
			}			
		}
		for(int i=0;i<y.effects.Count;i++){
			if(y.effects[i].type == status_effects_names.defense_change){
				defense = defense + y.effects[i].amt;
			}			
			if(y.effects[i].type == status_effects_names.color_damage_mod){
				color_weakness *= y.effects [i].amt;
			}
			if(y.effects[i].type == status_effects_names.reflect){
				reflected_damage *= y.effects [i].amt;
			}
		}
		float base_damage = attack - defense;

			if (x.color == "red" && y.color == "blue") {
			base_damage *= color_weakness;
			}
			if (x.color == "blue" && y.color == "green") {
			base_damage *= color_weakness;
			}
			if (x.color == "green" && y.color == "red") {
			base_damage *= color_weakness;
			}
				// if we do reflected damage, we can add this
		return new combat_result (reflected_damage,base_damage);
	}
    private void OnMouseUp()
    {
		Debug.Log("Clicked (tile's onmouseup)");

		GameObject p1 = gameObject.transform.parent.gameObject;
		bool test = tile_type == TileType.move ;
		Debug.Log ("Stunned?: " + test);

	    if (tile_type == TileType.move && p1.GetComponent<Unit>().ps.root_turns == 0 && p1.GetComponent<Unit>().ps.stun_turns == 0  ) { // can move

            if (p1.GetComponent<Unit>().hasMoved)
            {
                Debug.Log("can't move bro");
                return;
            }
            p1.GetComponent<Unit>().isChosen = false;
            p1.GetComponent<Unit>().hasMoved = true;
			Debug.Log ("isRange is true");
			moveUnitToTile (p1);
			GameObject[] gameObjects;
			gameObjects = GameObject.FindGameObjectsWithTag ("Range");

			for (var i = 0; i < gameObjects.Length; i++) {
				Destroy (gameObjects [i]);
			}
		} else if (tile_type == TileType.attack && p1.GetComponent<Unit>().ps.disarm_turns == 0 && p1.GetComponent<Unit>().ps.stun_turns == 0 ) { // can attack

			// get the attacker, get the enemy, and call the attack function
			Debug.Log ("attacker x: " + p1.transform.position.x + " y: " + p1.transform.position.y);
			Debug.Log ("target x: " +Mathf.RoundToInt(this.transform.position.x) + " y: " + Mathf.RoundToInt(this.transform.position.y));

			string attackerTeam = p1.GetComponent<Unit> ().team;
            if (p1.GetComponent<Unit>().hasAttacked)
                return;

            p1.GetComponent<Unit>().isChosen = false;

            p1.GetComponent<Unit>().hasAttacked = true;

            foreach (Transform child in p1.GetComponent<Unit>().transform)
            {
                if (child.gameObject.tag != "Particle" && child.gameObject.tag != "StunParticle")
                    GameObject.Destroy(child.gameObject);
            }


			GameObject[] allPlayers = GameObject.FindGameObjectsWithTag ("Player");
			Debug.Log ("length of all players " + allPlayers.Length);

			GameObject enemy;
			for (int i = 0; i < allPlayers.Length; i++) {
				enemy = allPlayers [i];
				Debug.Log ("Parameters of enemy " + enemy.transform.position.x + " " + enemy.transform.position.y);
				if (Mathf.RoundToInt(enemy.transform.position.x) == Mathf.RoundToInt( this.transform.position.x) && Mathf.RoundToInt( enemy.transform.position.y) == Mathf.RoundToInt(this.transform.position.y)) {
					// both players take damage
					combat_result result = combat (p1.GetComponent<Unit> ().ps, enemy.GetComponent<Unit>().ps);
					Debug.Log (result.y_damage);
					//decrease health
					p1.GetComponent<Unit> ().ps._curHealth -= result.x_damage;
					enemy.GetComponent<Unit> ().ps._curHealth -= result.y_damage;
					Debug.Log (result.y_damage);

					fadePlayers (p1, enemy);


					if (p1.GetComponent<Unit> ().ps._curHealth <= 0) {
						//maybe they can revive, but for now destroy it
						gameManager.GetComponent<GameManager>().safelyDestroyUnits(p1);
					}
					if (enemy.GetComponent<Unit> ().ps._curHealth <= 0) {
						//maybe they can revive, but for now destroy it.
						gameManager.GetComponent<GameManager>().safelyDestroyUnits(enemy);
					}
					break;
				}
			}

		}
    }

    public void moveUnitToTile(GameObject unit) {
	    Debug.Log ("Click, x: " + this.x + " y:" + this.y);
		gameManager.MoveSelectedUnitTo (this.x, this.y, unit);
	}

	public void fadePlayers(GameObject unit1GO, GameObject unit2GO) {
		Unit unit1 = unit1GO.GetComponent<Unit> ();
		Unit unit2 = unit2GO.GetComponent<Unit> ();
		SpriteRenderer sprite1 = unit1GO.GetComponent<SpriteRenderer> ();
		SpriteRenderer sprite2 = unit2GO.GetComponent<SpriteRenderer> ();
		float healthPercentage1 = unit1.ps._curHealth / unit1.ps.maxHealth;
		float healthPercentage2 = unit2.ps._curHealth / unit2.ps.maxHealth;

		Debug.Log ("Fading players: p1 ratio: " + healthPercentage1 + " p2 ratio: " + healthPercentage2);
		// fades color to white unit 1 and unit 2 based on their health
		float incrementVal1 = Mathf.SmoothStep(0.0f, 1.0f, 1-healthPercentage1);
		float incrementVal2 = Mathf.SmoothStep(0.0f, 1.0f, 1-healthPercentage2);
		sprite1.color = new Color (sprite1.color.r + incrementVal1, sprite1.color.g + incrementVal1, sprite1.color.b + incrementVal1, sprite1.color.a + incrementVal1);
		sprite2.color = new Color (sprite2.color.r + incrementVal2, sprite2.color.g + incrementVal2, sprite2.color.b + incrementVal2, sprite2.color.a + incrementVal2);

	}

}

