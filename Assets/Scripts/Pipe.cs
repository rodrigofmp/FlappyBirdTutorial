using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float Speed = 1f;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsAlive)
        { 
            Vector3 pos = this.transform.position;
            pos.x = pos.x - Speed * Time.deltaTime;
            this.transform.position = pos;

            if (this.transform.position.x < -5f)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
