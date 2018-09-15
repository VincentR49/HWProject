using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject {

    public List<T> Items = new List<T>();

    public void Add(T t)
    {
        if (!Items.Contains(t))
        {
            Items.Add(t);
        }
    }
	
    public void Remove(T t)
    {
        if (Items.Contains(t))
        {
            Items.Remove(t);
        }
    }
	
	public void SwapPosition (int index1, int index2)
	{
		if (IsValidIndex(index1) && IsValidIndex(index2))
		{
			T temp = Items[index1];
			Items[index1] = Items[index2];
			Items[index2] = temp;
		}
        else
        {
            Debug.Log("Error: invalid indexes (" + index1 + " and " + index2 + ")");
        }
	}
	
	public void PutAtTheEnd(int index)
	{
        MoveElementTo(index, Items.Count - 1);
	}
	
	public void PutAtFirst(int index)
	{
        MoveElementTo(index, 0);
	}

    public void MoveElementTo(int oldIndex, int newIndex)
    {
        if (IsValidIndex(oldIndex) && IsValidIndex(newIndex))
        {
            T temp = Items[oldIndex];
            Items.RemoveAt(oldIndex);
            Items.Insert(newIndex, temp);
        }
        else
        {
            Debug.Log("Invalid index");
        }
    }

	public bool IsValidIndex(int index)
	{
		return Items != null && index >= 0 && index < Items.Count;
	}
}
