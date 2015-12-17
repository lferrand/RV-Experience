/* © 2015 Studio Pepwuper http://www.pepwuper.com */

using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	public GameObject Cursor; // Google Cardboard SDK: Cursor / GazePointer from CardboardMain Prefab
	private Vector3 goal;
    private Vector3 moveDirection;
    private CharacterController charac;
    private bool gazed;
    private bool gazedEN;
    public bool moveEnabled = true;
    public float timerDisable = 0.0f;
    public float timeToDisable = 2.0f;
    public float gravity = 20.0F;
    public float findTimer = 0.0f;
    public float waitTime = 5.0f;
    public float speed = 5.0f;
    private AudioSource footstep;
    private bool moving = false;
    Vector3 previousPosition;
    GameObject stopButton;
    GameObject enableButton;

	
	void Start() {
		this.goal = new Vector3(0f, 0f, 0f);
        this.charac = GetComponent<CharacterController>();
        this.footstep = GetComponent<AudioSource>();
        gazed = false;
        gazedEN = false;
        stopButton = GameObject.FindGameObjectWithTag("StopMovement");
        enableButton = GameObject.FindGameObjectWithTag("EnableMovement");
        enableButton.SetActive(false);
    }

    void Update()
    {
        if (moveEnabled)
        {
            FindMoveHelper();
            if (findTimer >= waitTime)
            {
                MoveToDestination();
                findTimer = waitTime;
            }
        }
        
        Vector3 movement = this.previousPosition - transform.position;
        if (movement.magnitude > 0.01)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if(moving)
        {
            footstep.mute = false;
        }
        else
        {
            footstep.mute = true;
        }

        if (gazed)
        {
            RemoveMovementFromGaze();
        }
        if (gazedEN)
        {
            RestoreMovementFromGaze();
        }
        previousPosition = transform.position;
    }
	
	//Set navigation destination to the position of the cursor
	//Ex. Call this from an event trigger on the floor object
	
	void MoveToDestination(){
        //this.agent.destination = goal;
        Vector3 direction = goal - transform.position;
        direction.y = 0.0f;
        if (direction.magnitude > 0.5)
        {
            charac.SimpleMove(direction.normalized * speed);
        }

    }

    public void SetGazedAtMotor(bool gazedAt)
    {
        gazed = gazedAt;
    }
    public void SetGazedAtMotorEnable(bool gazedAt)
    {
        gazedEN = gazedAt;
    }

    void RemoveMovementFromGaze()
    {
        if(timerDisable < timeToDisable)
        {
            timerDisable += Time.deltaTime;
        }
        else
        {
            gazed = false; 
            moveEnabled = false;
            stopButton.SetActive(false);
            enableButton.SetActive(true);
        }
    }
    void RestoreMovementFromGaze()
    {
        if (timerDisable > 0.0f)
        {
            timerDisable -= Time.deltaTime;
        }
        else
        {
            gazedEN = false;
            moveEnabled = true;
            stopButton.SetActive(true);
            enableButton.SetActive(false);
        }
    }

    void FindMoveHelper()
    {
        Ray ray = new Ray(Cursor.GetComponentInParent<CardboardHead>().transform.position, Cursor.GetComponentInParent<CardboardHead>().transform.forward);
        Debug.DrawRay(Cursor.GetComponentInParent<CardboardHead>().transform.position, Cursor.GetComponentInParent<CardboardHead>().transform.forward*5);
        RaycastHit hit;
        int layerMask = 1 << 5;
        //Debug.Log(Physics.Raycast(ray, out hit, 6.0f, 5));
        if (!Physics.Raycast(ray, out hit, 6.0f, layerMask))
        {
            layerMask = 1 << 8;
            if (Physics.Raycast(ray, out hit, 6.0f, layerMask))
            {
                if (hit.collider.CompareTag("CameraTool"))
                {
                    RaycastHit pointHit;
                    findTimer += Time.deltaTime;
                    ray = new Ray(hit.collider.transform.position, Vector3.down);
                    Debug.DrawRay(hit.collider.transform.position, Vector3.down);
                    if (GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider>().Raycast(ray, out pointHit, Mathf.Infinity))
                    {
                        goal = pointHit.point;
                    }
                    else
                    {
                        ray = new Ray(hit.collider.transform.position, Vector3.up);
                        Debug.DrawRay(hit.collider.transform.position, Vector3.up);
                        if (GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider>().Raycast(ray, out pointHit, Mathf.Infinity))
                        {
                            goal = pointHit.point;
                        }
                        else
                        {
                            goal = new Vector3(0f, 0f, 0f);
                            findTimer = 0;
                        }
                    }

                }
                else
                {

                    goal = new Vector3(0f, 0f, 0f);
                    findTimer = 0;
                }
            }
            else
            {
                goal = new Vector3(0f, 0f, 0f);
                findTimer = 0;
            }
        }
        else
        {
            goal = new Vector3(0f, 0f, 0f);
            findTimer = 0;
        }
    }
}