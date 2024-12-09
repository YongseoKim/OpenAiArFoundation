using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class DEBUG_FPS_COUNTER : MonoBehaviour
{
    [SerializeField]
    private int     _maxFpsSamples      = 10;
    [SerializeField]
    private float   _fpsUpdateInterval  = 0.05f;
    
    [SerializeField]
    private TextMeshProUGUI _fpsAvgCounter;
    [SerializeField]
    private TextMeshProUGUI _fpsLowCounter;
    
    private List<float> _fpsSamples     = new List<float>();
    
    private void Start()
    {
        Application.targetFrameRate = 120;
        StartCoroutine(SampleFPS());
    }

    private IEnumerator SampleFPS()
    {
        while (true)
        {
            _fpsSamples.Add(1f / Time.unscaledDeltaTime);
            if (_fpsSamples.Count >= _maxFpsSamples)
            {
                _fpsSamples.RemoveAt(0);
                UpdateFPSCounter((int)_fpsSamples.Average(), (int)_fpsSamples.Min());
            }
            
            yield return new WaitForSeconds(_fpsUpdateInterval);
        }
    }

    private void UpdateFPSCounter(int newAverage, int newLow)
    {
        _fpsAvgCounter.text = "AVG // " + newAverage.ToString();
        _fpsLowCounter.text = "LOW // " + newLow.ToString();
    }
}
