using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public Player player1;
    public eyeBat eyeBatToCreate;
    protected eyeBat[] eyeBatArray;
    int maxeyeBat = 100;

    // Start is called before the first frame update
    void Start()
    {
        eyeBatArray = new eyeBat[maxeyeBat];
        for (int i = 0; i < maxeyeBat; i++)
        {
            eyeBatArray[i] = null;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            makeEyeBat(Random.Range(-25.0f,25.0f),Random.Range(0.0f,10.0f),60);
        }

    }

    public void makeEyeBat(float x, float y,float z)
    {
        for (int i = 0; i < maxeyeBat; i++)
        {
            if (eyeBatArray[i] == null)
            {
                eyeBatArray[i] = Instantiate(eyeBatToCreate);
                eyeBatArray[i].initialize(x,y,z);
                eyeBatArray[i].player1 = this.player1;
                break;
            }
        }
    }

    public bool isEmptyEyeBat()
    {
        for (int i = 0; i < maxeyeBat; i++)
        {
            if (eyeBatArray[i] != null)
            {
                return false;
            }
        }
        return true;

    }
    public void allDelete()
    {
        for (int i = 0; i < maxeyeBat; i++)
        {
            if (eyeBatArray[i] != null)
            {
                Destroy(eyeBatArray[i]);
            }
        }
    }
}