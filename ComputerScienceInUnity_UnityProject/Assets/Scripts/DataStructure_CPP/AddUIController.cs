using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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

    private List<IListWrapper> _listWrappers = new List<IListWrapper>(){new ArrayListWrapper(), new ListObjectWrapper(), new ListIntWrapper(), new LinkedListWrapper()};
    
    private int _coroutineDoneCount = 0;
    private int _totalCoroutineCount = 4;
        
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
        
        _executeButton.interactable = false;
        int index = 0;
        
        foreach (var wrapper in _listWrappers)
        {
            Debug.Log(index);
            int cur = index;
            Task.Run(() => wrapper.MeasurePerformance(repeatCount, wrapper.AddRepeatedly, _sticks[cur]));
            index++;
        }
    }
    
    private IEnumerator ChangeStickHeight(GameObject stick, float elapsedMilliseconds, int interval, int duration)
    {
        Debug.Log(elapsedMilliseconds);
        float dividedTime = elapsedMilliseconds / interval;
        string text = elapsedMilliseconds.ToString();
        while (elapsedMilliseconds > 0)
        {
            // 높이:시간 = 1:10 (ex 오브젝트의 height가 100이라면 1000ms를 의미)
            stick.GetComponent<RectTransform>().sizeDelta += new Vector2(0, dividedTime/10);
            elapsedMilliseconds -= dividedTime;
            yield return new WaitForSeconds((float)duration/interval);
        }
        stick.GetComponent<BarController>().TimeText.text = text;
        
        _coroutineDoneCount++;
        if (_coroutineDoneCount == _totalCoroutineCount)
        {
            _coroutineDoneCount = 0;
            _executeButton.interactable = true;
        }
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
