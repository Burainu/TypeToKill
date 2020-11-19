using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public int hp;
    public Transform castle;
    public float speed;
    public string word;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        castle = GameObject.FindGameObjectWithTag("Castle").transform;
        
        SetWord();
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

    public void SetWord()
    {
        GameObject go = GameObject.Find("SpawnController");
        SpawnController other = (SpawnController)go.GetComponent(typeof(SpawnController));
        int level = 1;
        word = other.ReturnWord(level);
       // print("Enemy word:"+word);
    }
    public string ReturnWord()
    {
        return word;
    }
}
