using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject cam1, cam2;
    private Camera camera1, camera2;

    private int maxBullet = 10000;
    public Bullet bulletToCreate;
    private Bullet[] bulletArray;

    public float power = 500;

    public AudioClip gunSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        camera1 = cam1.GetComponent<Camera>();
        camera2 = cam2.GetComponent<Camera>();

        bulletArray = new Bullet[maxBullet];
        for (int i = 0; i < maxBullet; i++)
        {
            bulletArray[i] = null;

        }

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeBullet(float x,float y, int displayID) {

        audioSource.PlayOneShot(gunSound);

        Vector3 tin;//計算後座標をぶち込む変数
        if (displayID == 1)
        {
            tin = camera1.ScreenToWorldPoint(new Vector3(x, y, 15.0f));
        }
        else{//if(displayID==2)
            tin = camera2.ScreenToWorldPoint(new Vector3(x, y, 15.0f));
        }
        for (int i = 0; i < maxBullet; i++)
        {
            if (bulletArray[i] == null)
            {
                bulletArray[i] = Instantiate(bulletToCreate, Camera.main.transform.position, Quaternion.identity);

                Rigidbody rb = bulletArray[i].GetComponent<Rigidbody>();
                Vector3 force = new Vector3(tin.x - Camera.main.transform.position.x, tin.y - Camera.main.transform.position.y, tin.z - Camera.main.transform.position.z);
                rb.AddForce(power * force);

                break;

            }
        }
    }
}
