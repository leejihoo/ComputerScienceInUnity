using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackWrapper<T>
{
    public int Count { get; }
    public bool IsEmpty { get; }
    public void Push(int count,T item);
    public void Pop(int count);
    public bool Contains(T target);
    public T Peek();
    public void Clear();
}
