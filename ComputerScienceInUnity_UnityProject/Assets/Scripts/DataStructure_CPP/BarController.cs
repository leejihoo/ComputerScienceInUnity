using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;

    public TMP_Text TimeText
    {
        get => _timeText;
    }
}
