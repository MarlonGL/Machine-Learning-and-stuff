using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class SphereMover : Agent
{
    public GameObject particlePrefab;
    public EnviromentControl Control;
    public Rigidbody2D rb;
    public float steerForce;
    public float initialForce;
    float movement = 0f;
    public float speed;
    int score;
    [SerializeField()] GameObject lastPlataform;
    GameObject noPlatform;

    Vector3 initialPosition;
    void Start()
    {
        /*lastPlataform = new GameObject();
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;*/
        //rb.AddForce(Vector3.up * initialForce);
    }

    public override void Initialize()
    {
        noPlatform = new GameObject();
        lastPlataform = noPlatform;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.velocity.y);
        sensor.AddObservation(rb.velocity.x);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(0.1f);
        if(Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            SteerLeft();
        }
        else if(Mathf.FloorToInt(vectorAction[0]) == 2)
        {
            SteerRight();
        }
    }

    public override void OnEpisodeBegin()
    {
        //reset
        ResetBot();
    }
    public override void Heuristic(float[] actionsOut)
    {
        //player input
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            actionsOut[0] = 2;
        }
    }

    void Update()
    {
        //movement = Input.GetAxis("Horizontal") * speed;
       
    }
    void FixedUpdate()
    {
        //CheckInput();

    }
    void ResetBot()
    {
        Control.ResetEnviroment();
        rb.velocity = new Vector2(0, 0);
        lastPlataform = noPlatform;
        transform.position = initialPosition;
        score = 0;
        Control.updateScore(score);
    }
    public void CallEndEpisode()
    {
        AddReward(-1.0f);
        //Debug.Log(GetCumulativeReward());
        EndEpisode();
    }
    void SteerLeft()
    {
        
        Vector2 velocity = rb.velocity;
        velocity.x = -speed;
        rb.velocity = velocity;
    }

    void SteerRight()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = speed;
        rb.velocity = velocity;
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SteerLeft();

        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            SteerRight();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Plataform")
        {
            if(lastPlataform != collision.gameObject && collision.relativeVelocity.y>=0)
            {
                int newScore = (int)Vector2.Distance(lastPlataform.transform.position, collision.gameObject.transform.position);
                score += newScore;
                lastPlataform = collision.gameObject;
                AddReward(0.2f);
                GameObject particle = Instantiate(particlePrefab, gameObject.transform.position, collision.transform.rotation);
                Destroy(particle, 3);
                //Debug.Log(GetCumulativeReward());
                Control.updateScore(score);
            }
        }
    }
}
