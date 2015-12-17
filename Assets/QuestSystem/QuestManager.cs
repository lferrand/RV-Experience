using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuestManager : MonoBehaviour{

	private static object padlock = new object ();

	private static QuestManager singleton;
    private static UIDialog uiDialog;

	public static QuestManager Instance() {
		lock(padlock) {
			if(singleton == null) 
				singleton = new QuestManager();

		}
		return singleton;
	}


	public void Clear() {
		quests.Clear ();
		currentQuest = null;
	}
    public List<Quest> quests;

    public Quest currentQuest;

	// Use this for initialization
	void Start () {
        uiDialog = GameObject.FindObjectOfType<UIDialog>();
		if (singleton == null)
			singleton = this;
		else {
			//Add a new set of quests at original singleton
			Instance ().quests.AddRange (quests);
			DestroyImmediate (this);
		}
		DontDestroyOnLoad (this);
	}

    

    // Update is called once per frame
    void Update () {
        if (HaveCurrentQuest())
        {
            ShowDialog();
            TestCurrentQuest();
        }
		else {
			Quest quest = LookForFirstQuest ();
			if(quest != null)
            {
				currentQuest = quest;
				quest.gameObject.SetActive (true);
				currentQuest.state = Quest.QuestState.ACTIVE;
				//UIQuestManager.Instance ().ChangeCurrentQuest (currentQuest);
			}
		}


	}

    public void CurrentDialogFinished()
    {
        currentQuest.dialogAlreadyShow = true;
        PlayerMotor player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        if(player.timerDisable <= 0)
        {
            player.moveEnabled = true;
        }
        else if (player.timerDisable >= player.timeToDisable)
        {
            player.moveEnabled = false;
        }
        
    }

    public void ShowDialog()
    {
        if (!currentQuest.dialogAlreadyShow && currentQuest.questDialog != null)
        {
            uiDialog.gameObject.SetActive(true);
            uiDialog.dialog = currentQuest.questDialog;
        }
    }

    public Quest LookForFirstQuest() {
		foreach (Quest q in quests) {;
			if(q.state == Quest.QuestState.WAITING || q.state == Quest.QuestState.ACTIVE)
				return q;
		}
		return null;
	}

	public bool HaveCurrentQuest() {
		return currentQuest != null;
	}

	public void TestCurrentQuest() {
		if(currentQuest.IsDone())
		{
            currentQuest.removeCompas();
            //UIQuestManager.Instance().ShowSuccess();
			currentQuest.gameObject.SetActive(false);
            currentQuest.gameObject.GetComponent<MoveToQuest>().targetObject.SetActive(false);
            ClearCurrentQuest();

        }
		else if(  currentQuest.IsFailed()) {
            currentQuest.removeCompas();
            //UIQuestManager.Instance().ShowFail();
			currentQuest.gameObject.SetActive(false);
            ClearCurrentQuest();
        }
	}

	public void ClearCurrentQuest() {
		currentQuest = null;
	}

	public void ChangeCurrentQuest(Quest quest) {
		if (currentQuest != null)
			currentQuest.state = Quest.QuestState.WAITING;
		currentQuest = quest;

	}


}
