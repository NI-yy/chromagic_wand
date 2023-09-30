using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HoneboneExtentions
{
    public static void Visualize(this Ray ray,float range)
    {
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    }
    public static Vector3 ToVector3(this Vector2 vector) { return new Vector3(vector.x, vector.y, 0); }

    public static bool CheckRaycastHit(this RaycastHit2D hit,string tagName)
    {
        return hit.collider != null && hit.collider.CompareTag(tagName);
    }
    
}
