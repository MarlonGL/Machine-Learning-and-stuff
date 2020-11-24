using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SliderControl : MonoBehaviour
{
    public GameObject Slider, Bot;// platPrefab, platParent, Bot;
    public BoxCollider2D boundingBox;
    public float offset, lerpSpeed,speedMod;
    public float spawnOffset;
    Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        boundingBox = GetComponent<BoxCollider2D>();
        initialPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Bot.transform.position.y > boundingBox.bounds.max.y)
        {
            
            Vector2 newPos = new Vector2(gameObject.transform.position.x, Bot.transform.position.y);
            speedMod = Vector2.Distance(gameObject.transform.position, newPos) ;
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, newPos, lerpSpeed * Time.deltaTime);
            //Slider.transform.position = Vector2.MoveTowards(Slider.transform.position, -newPos, lerpSpeed * Time.deltaTime);
            //gameObject.transform.position = newPos;
            //Slider.transform.position = newPos;
        }

        if(Bot.transform.position.x < boundingBox.bounds.min.x)
            {
            Bot.transform.position = new Vector2(boundingBox.bounds.max.x, Bot.transform.position.y);
        }

            else if (Bot.transform.position.x > boundingBox.bounds.max.x)
        {
            Bot.transform.position = new Vector2(boundingBox.bounds.min.x, Bot.transform.position.y);
        }

        if(Bot.transform.position.y<boundingBox.bounds.min.y)
        {
            Debug.Log("You DED");
            Bot.GetComponent<SphereMover>().CallEndEpisode();
            //SceneManager.LoadScene(0);
            //Destroy(Bot);
        }

    }
    public void resetSlider()
    {
        gameObject.transform.position = initialPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Agent")
        {
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Agent")
        {

            /*GameObject newPlat = Instantiate(platPrefab, platParent.transform);
            float Xmin = boundingBox.bounds.min.x;
            float Xmax = boundingBox.bounds.max.x;
            newPlat.transform.position = new Vector2(Random.Range(Xmin, Xmax), boundingBox.bounds.max.y + spawnOffset);
            */
        }
    }
}
