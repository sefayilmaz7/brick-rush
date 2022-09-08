using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeBrickPercentage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI defaultBrickText;
    [SerializeField] private TextMeshProUGUI fakeMetalText;

    private void Update()
    {
        fakeMetalText.text = defaultBrickText.text;
        fakeMetalText.color = defaultBrickText.color;
    }


}
