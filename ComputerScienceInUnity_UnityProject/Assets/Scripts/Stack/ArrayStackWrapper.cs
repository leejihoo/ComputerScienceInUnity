using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayStackWrapper<T> : IStackWrapper<T>
{
    private Stack<T> _stack = new Stack<T>();
    public int Count => _stack.Count;
    public bool IsEmpty => Count == 0;
    
    public void Push(int count, T item)
    {
        for (int i = 0; i < count; i++)
        {
            _stack.Push(item);
        }
    }

    public void Pop(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _stack.Pop();
        }
    }

    public bool Contains(T target)
    {
        return _stack.Contains(target);
    }

    public T Peek()
    {
        return _stack.Peek();
    }

    public void Clear()
    {
        _stack.Clear();
    }
}
