using System.Collections.Generic;

public class LinkedStack<T>
{
    private LinkedList<T> _linkedStack = new();

    public int Count => _linkedStack.Count;
    public bool IsEmpty => Count == 0;
    
    public void Push(T item)
    {
        _linkedStack.AddLast(item);
    }

    public T Pop()
    {
        var target = _linkedStack.Last.Value;
        // 지식: remove를 해도 객체가 바로 삭제되지는 않는다.
        _linkedStack.RemoveLast();
        return target;
    }

    public T Peek()
    {
        var target = _linkedStack.Last.Value;
        return target;
    }

    public void Clear()
    {
        _linkedStack.Clear();
    }

    public bool Contains(T item)
    {
        return _linkedStack.Contains(item);
    }
}
