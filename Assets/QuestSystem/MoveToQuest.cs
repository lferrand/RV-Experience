using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class MoveToQuest : Quest {
    public GameObject targetObject;
    public void Update()
    {
        if (IsActive())
        {
            targetObject.SetActive(true);
            //setCompasToQuest();
        }
    }

    void OnTriggerEnter(Collider other)
    {
		if(other.CompareTag("Player"))
			state = QuestState.DONE;
    }

    public override string GetDescription()
    {
        return "Déplacé votre <color=green><b>personnage</b></color> jusqu'à <b>l'objetif</b> en suivant le <color=red><b>compas</b></color> de votre créature.";
    }
}
