using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Debug = UnityEngine.Debug;

public class AddUIController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _repeatCount;
    [SerializeField] private List<GameObject> _sticks;
    [SerializeField] private Button _executeButton;
    [SerializeField] private Button _clearButton;

    private List<IListWrapper> _listWrappers = new List<IListWrapper>(){new ArrayListWrapper(), new ListObjectWrapper(), new ListIntWrapper(), new LinkedListWrapper()};
        
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
        ExecuteAllAsync(repeatCount).Forget();
    }
    
    private async UniTask ExecuteAllAsync(int repeatCount)
    {
        var tasks = new List<UniTask>();

        for (int i = 0; i < _listWrappers.Count; i++)
        {
            var wrapper = _listWrappers[i];

            var task = wrapper.MeasurePerformance(repeatCount, wrapper.AddRepeatedly, _sticks[i]);
            tasks.Add(task);
        }

        await UniTask.WhenAll(tasks);
        _clearButton.interactable = true;
        _executeButton.interactable = true;
    }

    public void ClearAll()
    {
        foreach (var wrapper in _listWrappers)
        {
            wrapper.Clear();
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
