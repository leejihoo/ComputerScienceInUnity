using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListObjectWrapper : IListWrapper
{
    private List<object> _list = new List<object>();
    
    public void AddRepeatedly(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            _list.Add(i);
        }
    }

    public void InsertRepeatedly(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            _list.Insert(_list.IndexOf(_list.Count/2),i);
        }
    }

    public void Clear()
    {
        _list.Clear();
    }

    public void RemoveRepeatedly(int repeatCount)
    {
        repeatCount = Mathf.Min(_list.Count, repeatCount);
        for (int i = 0; i < repeatCount; i++)
        {
            _list.Remove(_list.Count/2);
        }
    }

    public void Search(int target)
    {
        _list.Contains(target);
    }
}
