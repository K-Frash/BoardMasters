using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject movePositon;
    public float moveDistance;
    public Renderer playerSprite;
    public int moveRange = 1;

    bool isChosen = false;
    private void Start()
    {
        playerSprite = GetComponent<Renderer>();
        moveDistance = playerSprite.bounds.size.x;
    }

    private void OnMouseDown()
    {
        if (!isChosen) //todo: and if it's your turn.
        {

            isChosen = true;
            showRange();
        } else
        {
            isChosen = false;
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
	
    void showRange()
    {
        Vector3 playPos = gameObject.transform.position;
        for (int i = -1 * moveRange; i <= moveRange; i++)
        {
            for (int j = -1 * moveRange; j <= moveRange; j++)
            {
                int diff = moveRange - Mathf.Abs(i);
                Vector3 moveOption = new Vector3(playPos.x + i * moveDistance, playPos.y + j * moveDistance, playPos.z);
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int positiveJ = Mathf.Abs(j);
                if (positiveJ <= diff)
                {
                    GameObject child;
                    child = Instantiate(movePositon, moveOption, Quaternion.identity) as GameObject;
                    //child.transform.parent = gameObject.transform;
                    Tile tile = child.GetComponent(typeof(Tile)) as Tile;
                    tile.x = i;
                    tile.y = j;
                    tile.type = Type.LAND;
                    //GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
                    //tile.gameManager = gm.GetComponent<GameManager>();
                }
            }
        }
    }
}
