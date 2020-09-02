using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    Player player;
    float clock = 0f;

    public float TimeToCreate = 3f;
    public GameObject PipeBase;
    public float Range = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        clock = TimeToCreate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.IsAlive)
            return;

        if (!player.IsGameStarted)
            return;

        clock += Time.deltaTime;
        if (clock >= TimeToCreate)
        {
            clock = 0;

            Vector3 pos = this.transform.position;
            pos.y = pos.y - Random.Range(-Range, Range);
            GameObject.Instantiate(PipeBase, pos, Quaternion.identity);
        }
    }
}
