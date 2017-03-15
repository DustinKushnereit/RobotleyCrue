using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {

    public GameObject health; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void calculateHealthDrop()
    {
        int rand = Random.Range(0, 101); //random range is inclusive and exclusive

        if(rand <= 20)
        {
            health.GetComponent<Transform>().localScale = transform.localScale;

            Instantiate(health, transform.position, transform.rotation);
        }
    }
}
