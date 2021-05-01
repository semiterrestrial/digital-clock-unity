using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DigitController : MonoBehaviour
{
    // Start is called before the first frame update
    private readonly Dictionary<string, DigitState> digitScripts = new Dictionary<string, DigitState>();
    private string lastTime = "";
    private bool isInitialized = false;

    void Start()
    {
        var childScripts = GetComponentsInChildren<DigitScript>();
        foreach (var script in childScripts)
        {
            digitScripts.Add(script.gameObject.name, new DigitState(script, 0));
        }
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(1);
        updateDigits(getCurrentTimeAsString());
        isInitialized = true;
    }

    private string getCurrentTimeAsString()
    {
        var now = DateTime.Now;
        return $"{now.Hour:D2}{now.Minute:D2}{now.Second:D2}";
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialized)
        {
            var currentTime = getCurrentTimeAsString();
            if (currentTime != lastTime)
            {
                updateDigits(currentTime);
            }
        }
    }

    private void updateDigits(string currentTime)
    {
        Debug.Log("New time: "+currentTime);
        var hourTens = int.Parse(currentTime.Substring(0, 1));
        var hourOnes = int.Parse(currentTime.Substring(1, 1));

        var minuteTens = int.Parse(currentTime.Substring(2, 1));
        var minuteOnes = int.Parse(currentTime.Substring(3,1));

        var secondTens = int.Parse(currentTime.Substring(4, 1));
        var secondOnes = int.Parse(currentTime.Substring(5,1));

        SetIfNotCurrent("SecondOnes", secondOnes);
        SetIfNotCurrent("SecondTens", secondTens);
        SetIfNotCurrent("MinuteTens", minuteTens);
        SetIfNotCurrent("MinuteOnes", minuteOnes);
        SetIfNotCurrent("HourOnes", hourOnes);
        SetIfNotCurrent("HourTens", hourTens);
        lastTime = currentTime;
    }

    private void SetIfNotCurrent(string digitName, int value)
    {
        var state = digitScripts[digitName];
        if (state.CurrentValue != value)
        {
            state.CurrentValue = value;
            state.DigitScript.CurrentDigit = value;
        }
    }
}
