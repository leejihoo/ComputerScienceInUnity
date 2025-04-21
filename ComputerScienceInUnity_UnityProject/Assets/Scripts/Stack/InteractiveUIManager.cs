using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InteractiveUIManager : MonoBehaviour
{
    private StackFunctionType _currentFunctionType = StackFunctionType.Push;
    private List<IStackWrapper<int>> _stackWrappers = new (){ new LinkedListStackWrapper<int>(), new ArrayStackWrapper<int>()};
    [SerializeField] private int sf_item = 0;
    [SerializeField] private TMP_InputField sf_countInputField;
    [SerializeField] private List<GameObject> _stickGraphs;
    
    public void ExecuteButton_clicked()
    {
        List<UniTask> tasks = new List<UniTask>();
        for (int i = 0; i < _stackWrappers.Count; i++)
        {
            int temp = i;
            var task = ChangeStickGraph(_stackWrappers[temp], _currentFunctionType, _stickGraphs[temp]);
            tasks.Add(task);
        }

        UniTask.WhenAll(tasks);
    }
    
    public void ChangeStackFunctionType(int index)
    {
        _currentFunctionType = (StackFunctionType)index;
        Debug.Log(_currentFunctionType);
    }
    
    // 시간을 측정하는 함수
    private long CalculateTime(IStackWrapper<int> stackWrapper, StackFunctionType functionType)
    {
        if (!int.TryParse(sf_countInputField.text, out int count))
        {
            Debug.Log("숫자가 아닌 문자열을 입력했습니다.");
        }
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        switch (functionType)
        {
            case StackFunctionType.Push:
                stackWrapper.Push(count,sf_item);
                break;
            case StackFunctionType.Pop:
                stackWrapper.Pop(count);
                break;
            case StackFunctionType.Peek:
                stackWrapper.Peek();
                break;
            case StackFunctionType.Contains:
                stackWrapper.Contains(sf_item);
                break;
        }
        stopwatch.Stop();
        
        return stopwatch.ElapsedMilliseconds;
    }
    
    // 그래프를 변화시키는 함수
    private async UniTask ChangeStickGraph(IStackWrapper<int> stackWrapper, StackFunctionType functionType, GameObject graph)
    {
        long time = 0;
        await UniTask.RunOnThreadPool(() =>
        {
            time = CalculateTime(stackWrapper, functionType);
        });
        graph.GetComponentInChildren<TMP_Text>().text = time.ToString();
    }
}
