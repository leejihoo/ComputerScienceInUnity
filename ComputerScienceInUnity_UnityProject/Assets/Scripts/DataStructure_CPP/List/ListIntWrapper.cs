using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListIntWrapper : IListWrapper
{
    private List<int> _list = new List<int>();
    
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
}
