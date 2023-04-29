using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject common;
    public GameObject rare;
    public GameObject exotic;

    //list of gameobjects/spawners to choose from
    public GameObject[] spawners;

    //[SerializeField]
    private int maxSheep= 15;
    [SerializeField]
    private float SpawnTickRate = 10f;

    private int curSheep = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < spawners.Length; i++)
        {
            selectSheep(i);

        }
    }

    private void Update()
    {

        if(SpawnTickRate>0)
        {
            SpawnTickRate -= Time.deltaTime;
        }
        else 
        {
            if(maxSheep >= curSheep)
            {
                Debug.Log(curSheep);
                Debug.Log("sheep incoming");
                int randomI = Random.Range(0, 10);
                selectSheep(randomI);
            }

            SpawnTickRate = 5f;
        }
    }
    void selectSheep(int i)
    {
        int percent = Random.Range(0, 100);
        if(percent <80)
        {
            Spawn(common, i);
        }else if(percent >=80 && percent<95 )
        {
            Spawn(rare, i);
            Debug.Log("A Rare sheep has appeared");
        }
        else
        {
            Spawn(exotic, i);
            Debug.Log("An Exotic sheep has appeared");
        }
    }

    void Spawn(GameObject sheep, int i)
    {
        
        Instantiate(sheep, spawners[i].transform.position, Quaternion.Euler(0, Random.Range(0,360), 0));
        curSheep++;

    }



}
