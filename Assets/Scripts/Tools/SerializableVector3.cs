using UnityEngine;
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 vec3)
    {
        x = vec3.x;
        y = vec3.y;
        z = vec3.z;
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int((int)x, (int)y);
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}