/* © 2015 Studio Pepwuper http://www.pepwuper.com */

using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	public GameObject Cursor; // Google Cardboard SDK: Cursor / GazePointer from CardboardMain Prefab
	private Vector3 goal;
    private Vector3 moveDirection;
	private NavMeshAgent agent;
    private CharacterController charac;
    public float gravity = 20.0F;
    public float findTimer = 0.0f;
    public float waitTime = 5.0f;
    public float speed = 5.0f;
	
	void Start() {
		this.agent = GetComponent<NavMeshAgent>();
		this.goal = new Vector3(0f, 0f, 0f);
        this.charac = GetComponent<CharacterController>();

	}

    void Update()
    {
        FindMoveHelper();
        if(findTimer >= waitTime)
        {
            MoveToDestination();
            findTimer = waitTime;
        }
        
    }
	
	//Set navigation destination to the position of the cursor
	//Ex. Call this from an event trigger on the floor object
	
	void MoveToDestination(){
        //this.agent.destination = goal;
        var direction = goal - transform.position;
        direction.y = 0.0f;
        if (direction.magnitude > 0.5)
        {
            charac.SimpleMove(direction.normalized * speed);
        }

    }

    void FindMoveHelper()
    {
        Ray ray = new Ray(Cursor.GetComponentInParent<CardboardHead>().transform.position, Cursor.GetComponentInParent<CardboardHead>().transform.forward);
        Debug.DrawRay(Cursor.GetComponentInParent<CardboardHead>().transform.position, Cursor.GetComponentInParent<CardboardHead>().transform.forward*5);
        RaycastHit hit;
        int layerMask = 1 << 8;
        if (Physics.Raycast(ray,out hit, 6.0f, layerMask))
        {
            if (hit.collider.CompareTag("CameraTool"))
            {
                RaycastHit pointHit;
                findTimer += Time.deltaTime;
                ray = new Ray(hit.collider.transform.position, Vector3.down);
                Debug.DrawRay(hit.collider.transform.position, Vector3.down);
                if (GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider>().Raycast(ray, out pointHit, Mathf.Infinity)){
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
    }
}