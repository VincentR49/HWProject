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
	}
	
	public void PutAtTheEnd(int index)
	{
		if (IsValidIndex(index))
		{
			T temp = Items[index];
			Items.RemoveAt(index);
			Items.Add(temp);
		}
	}
	
	public void PutAtFirst(int index)
	{
		if (IsValidIndex(index))
		{
			T temp = Items[index];
			Items.RemoveAt(index);
			Items.Insert(0, temp);
		}
	}
	
	public bool IsValidIndex(int index)
	{
		return Items != null && index >= 0 && index < Items.Count;
	}
}
