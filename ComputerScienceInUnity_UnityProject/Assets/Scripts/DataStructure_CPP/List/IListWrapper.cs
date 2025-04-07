using System;
using System.Collections;
using System.Diagnostics;
using PimDeWitte.UnityMainThreadDispatcher;
using UnityEngine;
using Debug = UnityEngine.Debug;

public interface IListWrapper
{
    public void AddRepeatedly(int repeatCount);
    public void InsertRepeatedly(int repeatCount);
    public void Clear();
    public void RemoveRepeatedly(int repeatCount);

    public void MeasurePerformance(int repeatCount, Action<int> target, GameObject graphStick)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        target.Invoke(repeatCount);
        stopwatch.Stop();
        float measure = stopwatch.ElapsedMilliseconds;
        Debug.Log(GetType());
        UnityMainThreadDispatcher.Instance().Enqueue(ChangeStickHeight(graphStick, measure,300,3));
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
