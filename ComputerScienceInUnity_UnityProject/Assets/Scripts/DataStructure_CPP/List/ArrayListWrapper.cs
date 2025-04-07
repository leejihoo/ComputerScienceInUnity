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
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        _arrayList.Clear();
    }

    public void RemoveRepeatedly(int repeatCount)
    {
        throw new System.NotImplementedException();
    }
}
