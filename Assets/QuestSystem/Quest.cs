using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class Quest : MonoBehaviour {


	public string title = "defaultTitle";

	public virtual string GetTitle() {
		return title;
	}

	public virtual string GetDescription() {
		return "default description";
	}

    public enum QuestState { WAITING, ACTIVE, DONE, FAILED };

    public QuestState state = QuestState.WAITING;

	public bool dialogAlreadyShow = false;

	public Dialog questDialog;

    public bool IsDone() {
		return state == QuestState.DONE;
	}

	public bool IsFailed() {
		return state == QuestState.FAILED;
	}
    public bool IsActive()
    {
        return state == QuestState.ACTIVE;
    }

    public void setCompasToQuest()
    {
        CompasTowards compas = GameObject.FindObjectOfType<CompasTowards>();
        if (compas != null)
        {
            compas.questToGoTo = this;
        }
    }

    public void removeCompas()
    {
        CompasTowards compas = GameObject.FindObjectOfType<CompasTowards>();
        if (compas != null)
        {
            compas.questToGoTo = null;
        }
    }


    //method to overide if Quest is triggered by message
}
