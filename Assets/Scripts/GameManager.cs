using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Sheep common;
    public Sheep rare;
    public Sheep exotic;
    public GameObject Player;


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

    void Awake()
    {
        if(instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
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


        //begin the discard count/ call discard
        if (Vector3.Distance(Player.transform.position, sheep.transform.position) >= 15)
        {

        }
    }

    public void AddToPen(Sheep sheep)
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

    public void RemoveFromPen(Sheep sheep)
    {
        //sheepinPen-->add to sheepinWorld
        //remove form sheepinPen
        SheepInWOrld.Add(sheep);
        sheepInPen.Remove(sheep);
        //set sheep.incaged to false
        sheep.incaged = false;

    }

    public void Discard(Sheep sheep)
    {
        SheepInWOrld.Remove(sheep);
        Destroy(sheep.gameObject);

        //when we discrad one we replace it with another

        selectSheep(Random.Range(0, 10));


    }
}
