using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Utils {

	public static double GetNorm2(Vector2 vector)
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
		RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down);
        //Debug.DrawRay(position, Vector3.down, Color.green);
        //Debug.Log("Cast Ray at position: " + position.ToString());
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
	}
	
	public static Vector2 To2D(Vector3 vector)
	{
		return new Vector2 (vector.x, vector.y);
	}
	
	public static double GetDistance(Vector2 pos1, Vector2 pos2)
	{
		return Math.Sqrt (Math.Pow (pos1.x - pos2.x, 2) + Math.Pow (pos1.y - pos2.y, 2));
	}

    public static double Round(double value, int n)
    {
        double factor = Math.Pow(10, n);
        return Math.Round(value * factor) / factor;
    }

    public static bool Overlaps(this RectTransform rectTrans1, RectTransform rectTrans2, bool worldPosition)
    {
        float x1 = worldPosition ? rectTrans1.position.x : rectTrans1.localPosition.x;
        float x2 = worldPosition ? rectTrans2.position.x : rectTrans2.localPosition.x;
        float y1 = worldPosition ? rectTrans1.position.y : rectTrans1.localPosition.y;
        float y2 = worldPosition ? rectTrans2.position.y : rectTrans2.localPosition.y;

        Rect rect1 = new Rect(x1, y1, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(x2, y2, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }

    




}
