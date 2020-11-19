using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public int hp;
    public Transform castle;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        castle = GameObject.FindGameObjectWithTag("Castle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, castle.position, speed * Time.deltaTime);
    }
}
