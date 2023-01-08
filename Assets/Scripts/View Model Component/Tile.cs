using UnityEngine;

public class Tile : MonoBehaviour
{
    public const float stepHeight = 0.25f; //modifying tile height
    public Point pos; //tracking position
    public int height; //tracking height
    public Vector3 Center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } }//allows placing of object in the center of the top of the tile
    public GameObject content;//for holding some piece of content on a tile

    [HideInInspector] public Tile prev;//stores tile previously traversed to reach given tile
    [HideInInspector] public int distance;//stores number of tiles crossed to reach given tile

    void Match() //if position or height is modified, visually reflect new values
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }

    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }
    public void Load (Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
