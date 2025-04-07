using System.Collections.Generic;

public class LinkedListWrapper : IListWrapper
{
    private LinkedList<int> _linkedList = new LinkedList<int>();
    
    public void AddRepeatedly(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            _linkedList.AddFirst(i);
        }
    }

    public void InsertRepeatedly(int repeatCount)
    {
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        _linkedList.Clear();
    }

    public void RemoveRepeatedly(int repeatCount)
    {
        throw new System.NotImplementedException();
    }
}
