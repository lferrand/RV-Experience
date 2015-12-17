using UnityEngine;
using System.Collections;

public class CompasTowards : MonoBehaviour {

    public Quest questToGoTo;
    private Renderer[] compas;

    // Use this for initialization
    void Start () {
        compas = gameObject.GetComponentsInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (questToGoTo == null)
        {
            foreach(Renderer r in compas)
            {
                r.enabled = false;
            }
        }
        else
        {
            foreach (Renderer r in compas)
            {
                r.enabled = true;
            }
            transform.LookAt(questToGoTo.transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.forward,Vector3.up), Time.deltaTime * 500);
            
            
        }
	}
}
