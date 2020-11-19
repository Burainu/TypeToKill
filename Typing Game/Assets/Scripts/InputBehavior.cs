﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitWord);
        input.onEndEdit = se;
        input.Select();
        input.text = "";

    }

  private void SubmitWord(string arg0)
    {
        GameObject go = GameObject.Find("SpawnController");
        SpawnController other = (SpawnController)go.GetComponent(typeof(SpawnController));
        other.AttemptedKill(arg0);

    }
}