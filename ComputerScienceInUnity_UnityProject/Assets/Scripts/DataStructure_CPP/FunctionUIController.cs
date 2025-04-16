using System.Collections.Generic;
using UnityEngine;

namespace DataStructure_CPP
{
    public class FunctionUIController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _functionUIs;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        // 드롭다운 옵션 인덱스를 매개변수로 받는다.    
        public void DropDown_ValueChanged(int optionIndex)
        {
            //드롭다운에서 선택된 UI만 활성화시킨다.
            for (int i = 0; i < _functionUIs.Count; i++)
            {
                _functionUIs[i].SetActive(i == optionIndex);
            }
        }
    }
}
