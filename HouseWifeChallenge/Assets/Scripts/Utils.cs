using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	public static float GetNorm2(Vector2 vector)
    {
        return Mathf.Sqrt (Mathf.Pow (vector.x, 2) + Mathf.Pow (vector.y, 2));
    }
	
	public static Texture2D GenerateBasicTexture (Color color)
	{
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
		return texture;
	}
	
	public static void AddUnique<T> (this IList<T> self, T item) // à mettre dans Utils
	{
		if (!self.Contains(item))
		{
			self.Add(item);
		}
	}

	public static GameObject GetColliderAtPosition(Vector2 position)
	{
		RaycastHit hit = Physics2D.Raycast(position, -Vector2.up);
		return hit.collider;
	}
	
	public static Vector2 To2D(Vector3 vector)
	{
		return new Vector2 (vector.x, vector.y);
	}
}
