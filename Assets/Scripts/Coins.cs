using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public AudioClip myVoice;
    public float rotSpeed = 15f;
    public int price = 1;

    private LevelMenu lvlMenu;

    private void Awake()
    {
        lvlMenu = GameObject.Find("Canvas").GetComponent<LevelMenu>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Player")) 
        {
            other.GetComponent<AudioSource>().PlayOneShot(myVoice);
            lvlMenu.AddGold(price);
            Destroy(gameObject);
        }
    }
}
