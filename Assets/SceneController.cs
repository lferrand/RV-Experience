using UnityEngine;
using System.Collections;
using System;

public class SceneController : MonoBehaviour {

    private bool resetGazed;
    private bool finishGazed;
    private float resetGazedTimer = 0.0f;
    private float finishGazedTimer = 0.0f;
    // Use this for initialization
    void Start () {
        resetGazed = false;
        finishGazed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (resetGazed)
        {
            ResetSceneFromGaze();
        }
        else
        {
            resetGazedTimer = 0.0f;
        }
        if (finishGazed)
        {
            FinishSceneFromGaze();
        }
        else
        {
            finishGazedTimer = 0.0f;
        }
    }
    public void SetGazedAtFinish(bool gazedAt)
    {
        finishGazed = gazedAt;
    }

    private void FinishSceneFromGaze()
    {
        if (finishGazedTimer < 3)
        {
            finishGazedTimer += Time.deltaTime;
        }
        else
        {
            Application.LoadLevel("end");
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
            Application.LoadLevel(0);
        }
    }
}
