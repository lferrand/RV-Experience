/* © 2015 Studio Pepwuper http://www.pepwuper.com */

using UnityEngine;
using System.Collections;

public class MenuFollowPlayer : MonoBehaviour
{

    public GameObject Player;
    private Vector3 playerPosition;

    void LateUpdate()
    {
        playerPosition = Player.transform.position;
        Vector3 newPos = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
        transform.position = newPos;
    }
}
