using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyDamageZone : MonoBehaviour {

    public int hp = 10;


    public UnityEngine.UI.Text hpText;

    public GameObject GameOverEnemyUI;

    // Use this for initialization
    void Start () {
        hpText.text = hp.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    void OnTriggerEnter2D(Collider2D c)
    {

        // must be enemy
       hp -= 1;
       Destroy(c.gameObject);

        hpText.text = hp.ToString();

        if (hp <= 0)
        {
            GameOverByEnemy();
        }
    }

    void GameOverByEnemy()
    {
        GameOverEnemyUI.SetActive(true);
    }
}
