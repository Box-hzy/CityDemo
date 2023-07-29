using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class House : MonoBehaviour
{


    public int houseState;
    public List<House> neighbourHouses = new List<House>();
    public int score;
    public MeshRenderer meshRenderer;
    Material destroiedMaterial;
    Material burningMaterial;
    public Material[] defaultMaterial_Array;
    public Material[] NewMaterial_Array;
    [SerializeField]GameObject fireParticlePrefab;
    //public Transform escapePoint;
    public float radius = 10;    //raycast radius
    Collider[] results = new Collider[10];
    [SerializeField] LayerMask layerMask;
    float RecoverTime;
    float timer;
    Vector3 centrePoint; //transform.position is based on Pivot point, however the pivot point of the model is too faraway
    float FireTimer = 0;
    VisualEffect fireVFX;
    ParticleSystem fireParticle;
    ParticleSystem SparkParticle;

    [Header("HouseBuring")]
    float FireSpeed;
    float spreadTime;
    [SerializeField]float thisSpreaTime;

    [Header("Put Off Fire")]
    float putoffFireSpeed;
    float putoffFireTime = 0;
    bool isPutOff = false;
    GameManagement gameManagement;
    



    // Start is called before the first frame update
    void Start()
    {

        houseState = 0;
        layerMask = 1 << 10;
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial_Array = meshRenderer.materials;
        NewMaterial_Array = new Material[defaultMaterial_Array.Length];
        
        

        //gameObject.AddComponent<NavMeshObstacle>().carving = true;
        gameObject.AddComponent<NavMeshObstacle>().carving = false;
        centrePoint = GetComponent<MeshRenderer>().bounds.center;
        AddNeighbour();

        ////instantiate vfx component
        //fireParticlePrefab = GameObject.Find("FireParticlePrefab");
        //GameObject particle = Instantiate(fireParticlePrefab, centrePoint, Quaternion.identity, transform);

        //particle.transform.forward = Vector3.up;
        //fireVFX = GetComponentInChildren<VisualEffect>();
        fireParticle = GetComponentInChildren<ParticleSystem>();
        SparkParticle= fireParticle.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        FireSpeed = GetFireSpeed();
        spreadTime = gameManagement.getFireSpreadTime();
        putoffFireSpeed = gameManagement.getFiremanPutOffFireSpeed();
        thisSpreaTime = spreadTime;

        burningMaterial = gameManagement.getBurningMaterial();
        destroiedMaterial= gameManagement.getDestroiedMaterial();
        RecoverTime = gameManagement.getHouseRecoverTime();
        //putoffFire = fireVFX.GetFloat("MaxSize");
        //SetEscapePoint();

        if (GetComponentInChildren<SpawnScaredVillagers>() != null)
        {
            GetComponentInChildren<SpawnScaredVillagers>().thisHouse = this;
        }

    }


    float GetFireSpeed()
    {
        if (transform.parent.CompareTag("OldHouse"))
        {
            return gameManagement.getOldFireIncreaseSpeed();
        }
        else if (transform.parent.CompareTag("SmallHouse"))
        {
            return gameManagement.getSmallFireIncreaseSpeed();
        }
        else if (transform.parent.CompareTag("BigHouse"))
        {
            return gameManagement.getLargeFireIncreaseSpeed();
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (houseState == 1)
        { //fire will get big with the time increase;
            fireParticle.Play();
            SparkParticle.Play();
            FireTimer += Time.deltaTime;
            float CurrentFireSize = 10+FireTimer * FireSpeed;
            if (CurrentFireSize <= 70)
            { //maxfire
                var emission = fireParticle.emission;
                emission.rateOverTimeMultiplier = CurrentFireSize;
                var emission2 = SparkParticle.emission;
                emission2.rateOverTimeMultiplier = CurrentFireSize * 5 / 7;
            }
            else
            {
                var emission = fireParticle.emission;
                emission.rateOverTimeMultiplier = 70;
                var emission2 = SparkParticle.emission;
                emission2.rateOverTimeMultiplier = 50;

            }
            
            if (CurrentFireSize >= 60)
            {
                thisSpreaTime -= Time.deltaTime;
                if (thisSpreaTime <= 0)
                {
                    BurnNeighbour();
                }
            }


                /*if (CurrentFireSize <= fireVFX.GetFloat("MaxSize"))
                {
                    fireVFX.SetFloat("FireSize", CurrentFireSize);
                }
                else
                {
                    fireVFX.SetFloat("FireSize", fireVFX.GetFloat("MaxSize"));
                    //fireVFX.SetFloat("FireSize", 50);
                }*/








            }
        if (houseState == 2)
        {

            putoffFireTime += Time.deltaTime;
            //float CurrentFireSize = fireVFX.GetFloat("FireSize") - putoffFireSpeed * putoffFireTime;
            float CurrentFireSize = fireParticle.emission.rateOverTimeMultiplier - putoffFireSpeed * putoffFireTime;

            if (CurrentFireSize > 0)
            {
                var emission = fireParticle.emission;
                emission.rateOverTimeMultiplier = CurrentFireSize;
                var emission2 = SparkParticle.emission;
                emission2.rateOverTimeMultiplier = CurrentFireSize*5/7;
            }
            else
            {
                var emission = fireParticle.emission;
                emission.rateOverTimeMultiplier = 0;
                var emission2 = SparkParticle.emission;
                emission2.rateOverTimeMultiplier = 0;
                putoffFireTime = 0;
                houseState = 3;
                isPutOff = true;
            }

            /*if (CurrentFireSize > 0)
            {
                fireVFX.SetFloat("FireSize", CurrentFireSize);
            }
            else
            {
                fireVFX.SetFloat("FireSize", 0);
                putoffFireTime = 0;
                houseState = 3;
                isPutOff = true;
            }*/


        }



        if (houseState == 3)
        {
            timer -= Time.deltaTime;
            fireParticle.Stop();
            SparkParticle.Stop();
            if (timer <= 0)
            {
                setState(0);
                thisSpreaTime = spreadTime;
            }
        }


    }




    private void AddNeighbour()
    {
        int hits = Physics.OverlapSphereNonAlloc(centrePoint, radius, results, layerMask);
        for (int i = 0; i < hits; i++)
        {
            //Debug.Log("111");
            if (results[i].TryGetComponent<House>(out House house))
            {

                if (house != this)
                    neighbourHouses.Add(house);
            }
        }


    }

    void BurnNeighbour() {
        foreach (House house in neighbourHouses) {
            if (house.getState() == 0) {
                house.setState(1);
            } 
        }
    }

    public int getState()
    {
        return houseState;
    }

    public void setState(int thisState)
    {
        houseState = thisState;

        switch (houseState)
        {
            case 0://original state
                meshRenderer.materials = defaultMaterial_Array;
                break;

            case 1://fire is burning

                if (burningMaterial != null)
                {
                    for (int i = 0; i < NewMaterial_Array.Length; i++)
                    {
                        NewMaterial_Array[i] = burningMaterial;
                    }
                    meshRenderer.materials = NewMaterial_Array;
                }

                UImanager.instance.UpdateScore(score);
                isPutOff = false;
                break;
            case 2:// fireman is putting off the fire
                break;
            case 3://house into ruin

                if (destroiedMaterial != null)
                {
                    for (int i = 0; i < NewMaterial_Array.Length; i++)
                    {
                        NewMaterial_Array[i] = destroiedMaterial;
                    }
                    meshRenderer.materials = NewMaterial_Array;

                }
               
                timer = RecoverTime;

                break;
        }
    }

    public void setScore(int thisScore)
    {
        score = thisScore;
    }
    public int getScore()
    {
        return score;
    }

    public Vector3 getCentre()
    {
        return centrePoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centrePoint, radius);
    }

    public bool getIsPutOff()
    {
        return isPutOff;
    }



}
