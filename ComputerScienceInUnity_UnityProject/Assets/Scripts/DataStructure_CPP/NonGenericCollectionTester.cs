using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Unity.VisualScripting;
using Debug = UnityEngine.Debug;

namespace DataStructure_CPP
{
    public class NonGenericCollectionTester : MonoBehaviour
    {
        private ArrayList _arrayList;
        private List<object> _list;
        
        private ArrayList _arrayListForAdd;
        private List<object> _listForAdd;
    
        // Start is called before the first frame update
        void Start()
        {
            object[] objects = new object[] { 0, "hello", 5.5, 'c' };
            List<object> manyObjects = new List<object>();
            
            _arrayList = new ArrayList();
            _list = new List<object>();
            _arrayListForAdd = new ArrayList();
            _listForAdd = new List<object>();
            
            //10000000
            for (int i = 0; i < 1000000; i++)
            {
                manyObjects.AddRange(objects);
            }
            
            Stopwatch stopwatch = new Stopwatch();
            // stopwatch.Start();
            // for (int i = 0; i < 10000000; i++)
            // {
            //     _arrayList.AddRange(objects);
            // }
            // stopwatch.Stop();
            // Debug.Log("arrayList AddRange 속도: " + stopwatch.ElapsedMilliseconds);
            _arrayList.Clear();
            _list.Clear();
            
            stopwatch.Restart();
            for (int i = 0; i < 4; i++)
            {
                foreach (var target in manyObjects)
                {
                    _listForAdd.Add(target);
                }
            }   
            stopwatch.Stop();
            Debug.Log("list Add 속도: " + stopwatch.ElapsedMilliseconds);
            
            stopwatch.Restart();
            for (int i = 0; i < 4; i++)
            {
                _list.AddRange(manyObjects);
            }   
            stopwatch.Stop();
            Debug.Log("list AddRange 속도: " + stopwatch.ElapsedMilliseconds);
        

            #region 사이징된 AddRange

            // _arrayList.Clear();
            // _list.Clear();
            //
            // stopwatch.Start();
            // for (int i = 0; i < 10000000; i++)
            // {
            //     _arrayList.AddRange(objects);
            // }
            // stopwatch.Stop();
            // Debug.Log("사이즈 변경 후 arrayList AddRange 속도: " + stopwatch.ElapsedMilliseconds);
            //
            // stopwatch.Restart();
            // for (int i = 0; i < 10000000; i++)
            // {
            //     _list.AddRange(objects);
            // }   
            // stopwatch.Stop();
            // Debug.Log("사이즈 변경 후 list AddRange 속도: " + stopwatch.ElapsedMilliseconds);

            #endregion
            
            
            
            // stopwatch.Restart();
            // for (int i = 0; i < 10000000; i++)
            // {
            //     foreach (var target in objects)
            //     {
            //         _arrayListForAdd.Add(target);
            //     }
            // }
            // stopwatch.Stop();
            // Debug.Log("arrayList Add 속도: " + stopwatch.ElapsedMilliseconds);
            
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
