/* © 2015 Studio Pepwuper http://www.pepwuper.com */

using UnityEngine;
using System.Collections;

public class HUDFollowPlayer : MonoBehaviour
{

    public GameObject Player;
    private Vector3 playerPosition;

    void LateUpdate()
    {
        playerPosition = Player.transform.position;
        Vector3 newPos = new Vector3(playerPosition.x + 1f, playerPosition.y + 4f, playerPosition.z + 3f);
        transform.position = newPos;
        this.gameObject.layer = 5;
    }
}
