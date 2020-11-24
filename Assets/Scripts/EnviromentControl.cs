using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//enviroment eh o responsavel por spawnar as plataformas e matar no momento, eh o que engloba todo o ambiente de teste entao pode ser usado pra medir uso de plataforma e tempo sla


public class EnviromentControl : MonoBehaviour
{

   public GameObject platParent, platPrefab;
    public BoxCollider2D boundingBox;
    public float spawnTimer;
    public Transform bot;
    public SliderControl botBox;
    public CameraFollow cameraObj;
    public int Layers, platPerLayer, layerOffset, offsetMod;

    [SerializeField()] int score;
    public Text scoreText;

    //new Marlon
    int numberPlatforms = 40;
    float minY = .2f;
    float maxY = 1.7f;
    float minX, maxX;
    Vector3 spawnPos;
    Vector3 lastSpawn;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = new Vector3();
        boundingBox = GetComponent<BoxCollider2D>();
        minX = boundingBox.bounds.min.x + 2f;
        maxX = boundingBox.bounds.max.x - 2f;
        //simplificado
        //minX = boundingBox.bounds.min.x + 2.5f;
        //maxX = boundingBox.bounds.max.x - 2.5f;
        SpawnPlataform();

        //bot.GetComponent<SphereMover>().Control = this;
        //StartCoroutine(spawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(lastSpawn, bot.position) < 15)
        {
            Debug.Log("New Spawn");
            SpawnPlataform();
        }
    }

    void InitiateEnviroment()
    {
        float y = boundingBox.bounds.min.y + layerOffset;
        float xMin = boundingBox.bounds.min.x;
        float xMax = boundingBox.bounds.max.x;
        //Debug.Log("initiate");
    }

    void SpawnPlataform()
    {
        
        for(int i = 0; i <= numberPlatforms; i++)
        {
            spawnPos.y += Random.Range(minY, maxY);
            spawnPos.x = Random.Range(minX, maxX);
            Instantiate(platPrefab, spawnPos, Quaternion.identity, platParent.transform);
            if(i == numberPlatforms)
            {
                lastSpawn = spawnPos;
            }
        }
        /*GameObject newPlat = Instantiate(platPrefab, platParent.transform);
        float offset = Random.Range(-3.0f, 3.0f);
        float Xmin = boundingBox.bounds.min.x;
        float Xmax = boundingBox.bounds.max.x;
        newPlat.transform.position = new Vector2(Random.Range(Xmin, Xmax), boundingBox.bounds.max.y + offset);
        */
    }

    public void updateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void ResetEnviroment()
    {
        Debug.Log("Reset Enviroment");
        botBox.resetSlider();
        cameraObj.resetCamera();
    }
    IEnumerator spawnLoop()
    {
        yield return new WaitForSeconds(spawnTimer);

        SpawnPlataform();

        StartCoroutine(spawnLoop());
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Plataform")
        {
            Destroy(collision.gameObject);

        }
    }
}
