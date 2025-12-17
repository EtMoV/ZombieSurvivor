using System.Collections.Generic;

[System.Serializable]
public class TileCodeJson
{
    public int nextWall = 1;
    public int nextGround = 1;
    public List<TileCodeEntry> tiles = new List<TileCodeEntry>();
}
