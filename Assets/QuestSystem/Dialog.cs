using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public string speakerName;
    public Sprite speakerImage;

	public List<string> listOfText = new List<string>();


    public void RemoveFirst()
    {
		listOfText.RemoveAt(0);
    }

    public string NextDialog()
    {
        RemoveFirst();
		if(listOfText.Count>0)
			return getCurrentDialog ();
		return null;
    }

    public string getCurrentDialog()
    {
        return listOfText[0];
    }


}

