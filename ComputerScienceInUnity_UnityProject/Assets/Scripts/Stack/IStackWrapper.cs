using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackWrapper<T>
{
    public void Push(int count);
    public void Pop(int count);
    public bool Contains(T target);
    public T Peek();
    public void Clear();
}
