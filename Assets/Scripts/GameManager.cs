using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Sheep common;
    public Sheep rare;
    public Sheep exotic;


    //list of gameobjects/spawners to choose from
    public GameObject[] spawners;

    List<Sheep> SheepInWOrld = new List<Sheep>();
    
    //Sheep[] sheepInWorld;
    List<Sheep> sheepInPen = new List<Sheep>();

    //[SerializeField]
    private int maxSheep= 15;
    [SerializeField]
    private float SpawnTickRate = 10f;

    int count;
    int penCount;
    int PlayerWallet;
    private int curSheep;
    //curr = sheepInWorld.length- sheepInPen.length
    //total = sheepInWorld.length + sheepInPen.length



    // Start is called before the first frame update
    void Start()
    {
        PlayerWallet = 0;
        count = 0;
        penCount = 0; 
        for(int i = 0; i < 10; i++)
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
            if(maxSheep > SheepInWOrld.Count)
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

    void Spawn(Sheep sheep, int i)
    {
        
        Instantiate(sheep, spawners[i].transform.position, Quaternion.Euler(0, Random.Range(0,360), 0));
        //curSheep++;
        SheepInWOrld.Add(sheep);
    }

    void AddToPen(Sheep sheep)
    {
        //sheepingworld --> add to sheepinpen
        //remove from sheepingWorld
        SheepInWOrld.Remove(sheep);
        sheepInPen.Add(sheep);
        //set sheep.incaged to true
        sheep.incaged = true;
        Debug.Log("sheep in pen");
        penCount++;
    }

    void RemoveFromPen(Sheep sheep)
    {
        //sheepinPen-->add to sheepinWorld
        //remove form sheepinPen
        SheepInWOrld.Add(sheep);
        sheepInPen.Remove(sheep);
        //set sheep.incaged to false
        sheep.incaged = false;

    }

    void Discard(Sheep sheep)
    {
        //if sheep either not incaged and sold then discard
        if(!sheep.incaged || sheep.sold)
        {
            // if sold add value 
        }
        //check bool for both
    }

}
