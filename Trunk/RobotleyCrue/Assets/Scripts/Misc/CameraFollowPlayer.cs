using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public bool inMenu;

    void Start()
    {
        inMenu = true;
    }

    void Update()
    {
        //if (!inMenu)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
    }
}
