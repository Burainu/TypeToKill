using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 6;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEndGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hp--;
        Destroy(collision.gameObject);
        Debug.Log("hp - 1");
    }

    private void CheckForEndGame()
    {
        if (hp <= 0) {
            Debug.Log("GameOver");
        }
    }
}
