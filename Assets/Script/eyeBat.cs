using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeBat : Enemy
{
    public Player player1;

    private CharacterController characterController;
    private Animator animator;

    private AudioSource audioSource;
    public AudioClip appear, attack, death;

    private int state=0;
    private float stateTime;//そのSTATEになった時刻
    private float stateLength;//そのSTATEを維持する時間の長さ
    private Vector3 destination;
    private Quaternion look;
    public float speed = 0.1f;

    public void initialize(float x,float y,float z) {
        transform.position = new Vector3(x,y,z);
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(appear);
        transform.LookAt(player1.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && state!=1000)
        {
            state = 999;
            GetComponent<Animator>().SetTrigger("down");
            Destroy(this.gameObject,3.0f);
        }

        switch (state)
        {
            case 0://待機開始時間記録
                stateTime = Time.realtimeSinceStartup;
                stateLength = Random.Range(3.0f,4.0f);
                state=1;
                GetComponent<Animator>().SetBool("idleB",true);
                //transform.LookAt(player1.transform.position);
                look = Quaternion.LookRotation(player1.transform.position - transform.position);//ゆっくり向くため
                break;

            case 1://待機
                if (Time.realtimeSinceStartup - stateTime > stateLength) {
                    state = 2;
                    GetComponent<Animator>().SetBool("idleB", false);
                }
                transform.rotation = Quaternion.RotateTowards(transform.rotation, look, 50 * Time.deltaTime);//ゆっくり向く
                break;

            case 2://移動目的地決定
                if (transform.position.z > 0) {
                    destination = new Vector3(-transform.position.x, Random.Range(0, 1) * transform.position.y, transform.position.z - Random.Range(15.0f, 30.0f));
                }
                else
                {
                    destination = new Vector3(-transform.position.x, Random.Range(0, 1) * transform.position.y, transform.position.z + Random.Range(15.0f, 30.0f));

                }
                state =3;
                GetComponent<Animator>().SetBool("runB",true);
                look = Quaternion.LookRotation(destination - transform.position);//ゆっくり向くため
                break;

            case 3://移動
                if (Vector3.Distance(transform.position, player1.transform.position) < 18.0f){
                    state = 4;
                }
                if (speed *5> Vector3.Distance(transform.position, destination)){
                    state = 0;
                    GetComponent<Animator>().SetBool("runB", false);
                }else {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, look, 50*Time.deltaTime);//ゆっくり向く
                    transform.position = transform.position + speed*((destination - transform.position).normalized);
                }
                break;

            case 4://攻撃準備
                if (transform.position.z > 0) { destination = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 0.0f), 12.0f);
                }
                else
                {
                    destination = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 0.0f), -12.0f);
                }
                state = 5;
                look = Quaternion.LookRotation(player1.transform.position - transform.position);//ゆっくり向くため
                //transform.LookAt(player1.transform.position);
                break;

            case 5://攻撃posへ移動
                if (speed * 3 > Vector3.Distance(transform.position, destination)){
                    state = 6;
                    stateTime = Time.realtimeSinceStartup;
                    stateLength = Random.Range(0.5f, 1.0f);
                    transform.LookAt(player1.transform.position);
                }
                else{
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, look, 100 * Time.deltaTime);//ゆっくり向く
                    transform.position = transform.position + speed * ((destination - transform.position).normalized);
                }
                break;

            case 6://攻撃直前待機
                if (Time.realtimeSinceStartup - stateTime > stateLength)
                {
                    state = 7;
                    GetComponent<Animator>().SetTrigger("attack");
                    stateTime = Time.realtimeSinceStartup;
                    stateLength = 3.0f;

                    audioSource.PlayOneShot(attack);

                    player1.Dameged(1);
                }
                break;

            case 7://攻撃後隙
                if (Time.realtimeSinceStartup - stateTime > stateLength)
                {
                    state = 5;
                }
                break;

            case 999:
                audioSource.PlayOneShot(death);
                state = 1000;
                break;
            case 1000://死亡
                GetComponent<Animator>().Play("down");
                transform.position = transform.position - new Vector3(0, 0.1f,0);
                break;

        }
    }
}
