using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private LevelMenu lvlMenu;
    private bool hit = false;

    private void Awake()
    {
        lvlMenu = GameObject.Find("Canvas").GetComponent<LevelMenu>();
    }

    public void Hit()
    {
        if (hit == false) StartCoroutine(HitRespawn());
    }

    private IEnumerator HitRespawn()
    {        
        lvlMenu.SetPlayerHealth(hit);
        hit = true;
        yield return new WaitForSeconds(1);
        hit = false;
    }
}
