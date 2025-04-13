using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayListWrapper : IListWrapper
{
    private ArrayList _arrayList = new ArrayList();

    public void AddRepeatedly(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            _arrayList.Add(i);
        }
    }

    public void InsertRepeatedly(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            _arrayList.Insert(_arrayList.IndexOf(_arrayList.Count/2),i);
        }
    }

    public void Clear()
    {
        _arrayList.Clear();
    }

    public void RemoveRepeatedly(int repeatCount)
    {
        repeatCount = Mathf.Min(_arrayList.Count, repeatCount);
        for (int i = 0; i < repeatCount; i++)
        {
            _arrayList.Remove(_arrayList.Count/2);
        }
    }
}
