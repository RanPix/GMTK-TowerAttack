using TileMap;
using UnityEngine;

public class TileCreateArgs
{
    public TileType type { get; private set; }
    //[SerializeField] private Structure _structure;

    public TileCreateArgs(TileType type)
    {
        this.type = type;
    }
} // якщо нічого сюди більше не будемо додавати то видаляємо