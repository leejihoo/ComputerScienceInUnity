using System.Collections;
using System.Collections.Generic;
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
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        _list.Clear();
    }

    public void RemoveRepeatedly(int repeatCount)
    {
        throw new System.NotImplementedException();
    }
}
