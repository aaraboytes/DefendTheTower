﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToProceed : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(0))
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameover");
	}
}