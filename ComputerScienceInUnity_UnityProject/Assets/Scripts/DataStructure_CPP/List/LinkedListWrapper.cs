using System.Collections.Generic;
using UnityEngine;

public class LinkedListWrapper : IListWrapper
{
    private LinkedList<int> _linkedList = new LinkedList<int>();
    
    public void AddRepeatedly(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            _linkedList.AddLast(i);
        }
    }

    public void InsertRepeatedly(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            var target = _linkedList.Find(_linkedList.Count/2);
            _linkedList.AddAfter(target, 4);
        }
    }

    public void Clear()
    {
        _linkedList.Clear();
    }

    public void RemoveRepeatedly(int repeatCount)
    {
        repeatCount = Mathf.Min(_linkedList.Count, repeatCount);
        for (int i = 0; i < repeatCount; i++)
        {
            _linkedList.Remove(_linkedList.Count/2);
        }
    }

    public void Search(int target)
    {
        _linkedList.Contains(target);
    }
}
