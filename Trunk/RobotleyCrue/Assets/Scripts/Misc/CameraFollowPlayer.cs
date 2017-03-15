using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public bool inMenu;
    public float duration = 0.5f;
    public float magnitude = 0.1f;

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

    public IEnumerator Shake()
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(1.0f * percentComplete, 0.0f, 1.0f);
            
            float x = Random.Range(-1, 1) * 1.0f;
            float y = Random.Range(-1, 1) * 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            transform.position = new Vector3(x - 4.0f, originalCamPos.y, originalCamPos.z);

            yield return null;
        }

        transform.position = originalCamPos;
    }
}
