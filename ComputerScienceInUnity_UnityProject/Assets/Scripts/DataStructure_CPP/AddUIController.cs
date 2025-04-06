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
    
    private ArrayList _arrayList = new ArrayList();
    private List<object> _list = new List<object>();
    private List<int> _listInt = new List<int>();
    private LinkedList<int> _linkedList = new LinkedList<int>();
    
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
        Task task = new Task(()=>ExecuteArrayListAdd(repeatCount));
        task.Start();
        
        Task task2 = new Task(() => ExecuteListObjectAdd(repeatCount));
        task2.Start();
        
        Task task3 = new Task(() => ExecuteListIntAdd(repeatCount));
        task3.Start();
        
        Task task4 = new Task(() => ExecuteLinkedListIntAdd(repeatCount));
        task4.Start();
    }
    
    // 중복 코드 줄일 수 있음
    private void ExecuteArrayListAdd(int repeatCount)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < repeatCount; i++)
        {
            _arrayList.Add(i);
        }
        stopwatch.Stop();
        float measure = stopwatch.ElapsedMilliseconds;
        UnityMainThreadDispatcher.Instance().Enqueue(ChangeStickHeight(_sticks[0], measure,300,3));
    }
    
    private void ExecuteListObjectAdd(int repeatCount)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < repeatCount; i++)
        {
            _list.Add(i);
        }
        stopwatch.Stop();
        float measure = stopwatch.ElapsedMilliseconds;
        UnityMainThreadDispatcher.Instance().Enqueue(ChangeStickHeight(_sticks[1], measure,300,3));
    }
    
    private void ExecuteListIntAdd(int repeatCount)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < repeatCount; i++)
        {
            _listInt.Add(i);
        }
        stopwatch.Stop();
        float measure = stopwatch.ElapsedMilliseconds;
        UnityMainThreadDispatcher.Instance().Enqueue(ChangeStickHeight(_sticks[2], measure,300,3));
    }
    
    private void ExecuteLinkedListIntAdd(int repeatCount)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < repeatCount; i++)
        {
            _linkedList.AddFirst(i);
        }
        stopwatch.Stop();
        float measure = stopwatch.ElapsedMilliseconds;
        UnityMainThreadDispatcher.Instance().Enqueue(ChangeStickHeight(_sticks[3], measure,300,3));
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
        _arrayList.Clear();
        _list.Clear();
        _listInt.Clear();
        _linkedList.Clear();

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
