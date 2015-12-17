using UnityEngine;
using System.Collections;
using System;

public class SceneController : MonoBehaviour {

    private bool resetGazed;
    private float resetGazedTimer = 0.0f;
	// Use this for initialization
	void Start () {
        resetGazed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (resetGazed)
        {
            ResetSceneFromGaze();
        }
	}
    public void SetGazedAtScene(bool gazedAt)
    {
        resetGazed = gazedAt;
    }

    private void ResetSceneFromGaze()
    {
        if(resetGazedTimer < 5)
        {
            resetGazedTimer += Time.deltaTime;
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
