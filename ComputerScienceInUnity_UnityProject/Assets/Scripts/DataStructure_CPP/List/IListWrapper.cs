using System;
using System.Collections;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public interface IListWrapper
{
    public void AddRepeatedly(int repeatCount);
    public void InsertRepeatedly(int repeatCount);
    public void Clear();
    public void RemoveRepeatedly(int repeatCount);
    public void Search(int target);

    public async UniTask MeasurePerformance(int repeatCount, Action<int> target, GameObject graphStick)
    {
        Stopwatch stopwatch = new Stopwatch();
        float measure = 0f;
        
        // async 키워드를 사용해도 UniTask는 기본적으로 MainThread에서 작동되기 때문에
        // 비동기로 작동시키고 싶으면 UniTask.RunOnThreadPool()를 사용해야 한다.
        // 성능 측정 둘 중 하나 선택해서 실행해야 함.
        #region 비동기 성능 측정(백그라운드 스레드에서 작동) 
        await UniTask.RunOnThreadPool(()=>
        {
            stopwatch.Start();
            target.Invoke(repeatCount);
            stopwatch.Stop();
            measure = stopwatch.ElapsedMilliseconds;
        });
        #endregion
        
        #region 동기 성능 측정(메인스레드에서 작동) UI 멈춤

        // stopwatch.Start();
        // target.Invoke(repeatCount);
        // stopwatch.Stop();
        // measure = stopwatch.ElapsedMilliseconds;

        #endregion
        
        await ChangeStickHeight(graphStick, measure, 300, 3);
    }

    private IEnumerator ChangeStickHeight(GameObject stick, float elapsedMilliseconds, int interval, int duration)
    {
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
    }
}

public enum FunctionType
{
    Add,
    Insert,
    Remove,
    Search
}
