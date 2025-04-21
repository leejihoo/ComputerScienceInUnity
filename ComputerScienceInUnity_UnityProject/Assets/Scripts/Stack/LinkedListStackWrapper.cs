using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListStackWrapper<T> : IStackWrapper<T>
{
    private LinkedStack<T> _linkedStack = new LinkedStack<T>();

    public int Count => _linkedStack.Count;
    public bool IsEmpty => Count == 0;

    public void Push(int count, T item)
    {
        for (int i = 0; i < count; i++)
        {
            _linkedStack.Push(item);
        }
    }

    public void Pop(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _linkedStack.Pop();
        }
    }

    public bool Contains(T target)
    {
        return _linkedStack.Contains(target);
    }

    public T Peek()
    {
        return _linkedStack.Peek();
    }

    public void Clear()
    {
        _linkedStack.Clear();
    }
}
