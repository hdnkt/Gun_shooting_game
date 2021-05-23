using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHp=10;
    protected int hp;

    public Image img;
    public Image img2;

    public AudioClip damaged;
    protected AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
       this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
       this.img2.color = Color.Lerp(this.img2.color, Color.clear, Time.deltaTime);
    }

    public void Dameged(int damage)
    {
        audioSource.PlayOneShot(damaged);
        hp-=damage;
        Debug.Log("hp:"+hp);
        this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
        this.img2.color = new Color(0.5f, 0f, 0f, 0.5f);
    }

    public int getHp()
    {
        return hp;

    }
}
