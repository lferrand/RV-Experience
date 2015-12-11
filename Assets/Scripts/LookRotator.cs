using UnityEngine;
using System.Collections;

public class LookRotator : MonoBehaviour
{
    public GameObject goFollow;
    private float speed = 0.5f;

    void Start()
    {
    }

    void Update()
    {
        transform.position = goFollow.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(goFollow.transform.forward), Time.deltaTime * 1);
        this.gameObject.layer = 8;

    }
}