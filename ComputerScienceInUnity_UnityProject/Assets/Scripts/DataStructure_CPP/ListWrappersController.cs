using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Debug = UnityEngine.Debug;

public class ListWrappersController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _repeatCount;
    [SerializeField] private List<GameObject> _sticks;
    [SerializeField] private Button _executeButton;
    [SerializeField] private Button _clearButton;
    [FormerlySerializedAs("_listType")] [SerializeField] private FunctionType _functionType;

    private static List<IListWrapper> _listWrappers = new (){new ArrayListWrapper(), new ListObjectWrapper(), new ListIntWrapper(), new LinkedListWrapper()};
    //private Dictionary<FunctionType, Action<int>> _listFunction = new Dictionary<FunctionType, Action<int>>();

    private void Awake()
    {
        //_listFunction[FunctionType.Add] = 
    }

    public void ExecuteButton_Clicked()
    {
        ClearAll();
        
        int repeatCount;
        try
        {
            repeatCount = int.Parse(_repeatCount.text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        _clearButton.interactable = false;
        _executeButton.interactable = false;
        ExecuteAllAsync(repeatCount,_functionType).Forget();
    }
    
    private async UniTask ExecuteAllAsync(int repeatCount, FunctionType listType)
    {
        var tasks = new List<UniTask>();

        for (int i = 0; i < _listWrappers.Count; i++)
        {
            var wrapper = _listWrappers[i];

            UniTask task= default;
            
            switch (listType)
            {
                case FunctionType.Add:
                    task = wrapper.MeasurePerformance(repeatCount, wrapper.AddRepeatedly, _sticks[i]);
                    break;
                case FunctionType.Insert:
                    task = wrapper.MeasurePerformance(repeatCount, wrapper.InsertRepeatedly, _sticks[i]);
                    break;
                case FunctionType.Remove:
                    task = wrapper.MeasurePerformance(repeatCount, wrapper.RemoveRepeatedly, _sticks[i]);
                    break;
            }
            
            tasks.Add(task);
        }

        await UniTask.WhenAll(tasks);
        _clearButton.interactable = true;
        _executeButton.interactable = true;
    }

    public void ClearAll()
    {
        if (FunctionType.Add == _functionType)
        {
            foreach (var wrapper in _listWrappers)
            {
                wrapper.Clear();
            }    
        }
        
        foreach (var stick in _sticks)
        {
            // 높이 초기화
            var rectTransform = stick.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
            
            // 측정 시간 초기화
            stick.GetComponent<BarController>().TimeText.text = "";
        }
    }
}
