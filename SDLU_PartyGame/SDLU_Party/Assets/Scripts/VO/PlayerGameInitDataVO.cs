using System;
using UnityEngine;

[Serializable]
public class PlayerGameInitDataVO
{
    public int id;
    public Vector2 position; // x, z
    public Quaternion rotation;

    public PlayerGameInitDataVO(int id, Vector2 position, Quaternion rotation)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
    }
}