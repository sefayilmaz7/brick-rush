using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TakeScreenshot : MonoBehaviour
{
    public KeyCode actionKey = KeyCode.S;

    private string storageFolderName = "Screenshots";
    private string storageFolderPath => Path.Combine(Application.dataPath, storageFolderName);

#if UNITY_EDITOR
    private void Awake()
    {
        if (!Directory.Exists(storageFolderPath))
            Directory.CreateDirectory(storageFolderPath);
    }
#endif

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            string time = DateTime.Now.ToString("dd_M_h_mm_ss");
            ScreenCapture.CaptureScreenshot(Path.Combine(storageFolderPath, string.Format("{0}.png", time)));
        }
    }
#endif
}
