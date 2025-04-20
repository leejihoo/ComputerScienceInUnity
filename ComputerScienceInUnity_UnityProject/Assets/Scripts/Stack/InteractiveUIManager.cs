using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveUIManager : MonoBehaviour
{
    private StackFunctionType _currentFunctionType;

    public void ChangeStackFunctionType(int index)
    {
        _currentFunctionType = (StackFunctionType)index;
        Debug.Log(_currentFunctionType);
    }
    
}
