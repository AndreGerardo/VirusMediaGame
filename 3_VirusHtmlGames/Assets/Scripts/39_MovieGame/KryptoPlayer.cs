using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KryptoPlayer : MonoBehaviour
{
    [SerializeField] private float flightForce = 500f;
    [SerializeField] private float gravityScale = 3f;
    [SerializeField] private bool startFlight = false;

    private bool isHolding = false;
    private Rigidbody2D rb;


    public float lifetime = 5f; //lifetime of a point on the trail
    public float minimumVertexDistance = 0.1f;
    public Vector3 velocity; //direction the points are moving
    LineRenderer line;
    //position data
    List<Vector3> points;
    Queue<float> spawnTimes = new Queue<float>(); //list of spawn times, to simulate lifetime. Back of the queue is vertex 1, front of the queue is the end of the trail.


    private void CheckFlight()
    {
        if (Input.GetMouseButtonDown(0)) isHolding = true;
        if (Input.GetMouseButtonUp(0)) isHolding = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TrailConfiguration();
        if (MovieGameScript.instance.isPlaying)
        {
            CheckFlight();
            if (startFlight == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    rb.gravityScale = gravityScale;
                    MovieGameScript.instance.enemyCanSpawn = true;
                    startFlight = true;
                }
            }
            else
            {
                RotateKrypto();
                if (isHolding)
                {
                    rb.AddForce(Vector2.up * flightForce * Time.deltaTime);
                }
            }

            if (transform.position.y >= 4.49f || transform.position.y <= -4.49f) rb.velocity = Vector2.zero;

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 4.5f), transform.position.z);
        }
    }

    private void RotateKrypto()
    {
        Vector2 dir = rb.velocity;
        float angle = Mathf.Atan2(dir.y, 50f) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
    }


    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        points = new List<Vector3>() { transform.position }; //indices 1 - end are solidified points, index 0 is always transform.position
        line.SetPositions(points.ToArray());
    }

    void AddPoint(Vector3 position)
    {
        points.Insert(1, position);
        spawnTimes.Enqueue(Time.time);
    }

    void RemovePoint()
    {
        spawnTimes.Dequeue();
        points.RemoveAt(points.Count - 1); //remove corresponding oldest point at the end
    }

    private void TrailConfiguration()
    {
        //cull based on lifetime
        while (spawnTimes.Count > 0 && spawnTimes.Peek() + lifetime < Time.time)
        {
            RemovePoint();
        }

        //move positions
        Vector3 diff = -velocity * Time.deltaTime;
        for (int i = 1; i < points.Count; i++)
        {
            points[i] += diff;
        }

        //add new point
        if (points.Count < 2 || Vector3.Distance(transform.position, points[1]) > minimumVertexDistance)
        {
            //if we have no solidified points, or we've moved enough for a new point
            AddPoint(transform.position);
        }

        //update index 0;
        points[0] = transform.position;

        //save result
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            GetComponent<CapsuleCollider2D>().enabled = false;
            MovieGameScript.instance.EndSequence();
        }

        if (coll.CompareTag("Plate"))
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            transform.rotation = Quaternion.identity;
            GetComponent<CapsuleCollider2D>().enabled = false;
            MovieGameScript.instance.FinishSequence();
        }

        GetComponent<LineRenderer>().enabled = false;
    }

}
