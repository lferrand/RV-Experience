using UnityEngine;
using System.Collections;
using System;

public class MoveQuest : Quest {

    public bool up = false;
	public bool down = false;
	public bool left = false;
	public bool right = false;

    public bool Test()
    {
        return up && down && left && right;
    }

    // Update is called once per frame
    void Update () {
		if (state == QuestState.DONE || state == QuestState.WAITING)
			return;
        if (Input.GetAxis("Horizontal") > 0.0f)
            right = true;
        if (Input.GetAxis("Horizontal") < 0.0f)
            left = true;
        if (Input.GetAxis("Vertical") > 0.0f)
            up = true;
        if (Input.GetAxis("Vertical") < 0.0f)
            down = true;
		if (Test ())
			state = QuestState.DONE;
	}

	public override string GetDescription() {
		return "Utiliser les <b>touches du clavier</b> pour <color=green><b>déplacer</b></color> votre créature.";
	}
}
