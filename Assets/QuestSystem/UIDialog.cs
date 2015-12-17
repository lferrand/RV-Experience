using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDialog : MonoBehaviour {

	public Image speakerIcon;

	public Text speakerName;

	public Text dialogContent;

	public Dialog dialog;

	public void Update() {
		if (dialog == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            LoadDialog(dialog);
        }
            
	}

	public void LoadDialog(Dialog dialog) {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().moveEnabled = false;
		this.dialog = dialog;
		speakerIcon.sprite = dialog.speakerImage;
		speakerName.text = dialog.speakerName;
		dialogContent.text = dialog.getCurrentDialog();
	}

	public void Next() {
		string text = dialog.NextDialog();
		if (text == null) {
            QuestManager.Instance ().CurrentDialogFinished();
            dialog = null;
		} else {
			dialogContent.text = text;
		}
	}
}
