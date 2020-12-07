using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    public int hp;
    public GameObject hpObject;
    public GameObject dmgFlash;
    // Start is called before the first frame update
    void Start()
    {
        hp = 6;
        hpObject.GetComponent<UnityEngine.UI.Text>().text = "Castle Health: " + hp.ToString() ;

    }

    // Update is called once per frame
    void Update()
    {
        hpObject.GetComponent<UnityEngine.UI.Text>().text = "Castle Health: " + hp.ToString();
        CheckForEndGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hp--;
        Destroy(collision.gameObject);
        StartCoroutine(flashScreen());

    }
    IEnumerator flashScreen()
    {
        dmgFlash.SetActive(true);
        yield return new WaitForSeconds(.1f);
        dmgFlash.SetActive(false);
    }
    private void CheckForEndGame()
    {
        if (hp <= 0) {
            Debug.Log("GameOver");
        }
    }
}
