using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    #region MemberFields
    [SerializeField, Range(0.0f, 50.0f)]
    private float speed = 5f;
    [SerializeField, Range(0.0f, 400.0f)]
    private float steerSpeed = 180f;
    [SerializeField, Range(0.0f, 20.0f)]
    private float offset = 2.0f;
    [SerializeField]
    private int gap = 10;
    [SerializeField]
    private GameObject bodyPrefabs;
    [SerializeField]
    private List<GameObject> body = new List<GameObject>();


    public bool surrounded = false;
    public GameObject enemyCube = null;
    private float currentSpeed = 0;
    private List<Vector3> positionHistory = new List<Vector3>();
    #endregion MemberFields


    #region MonoBehaviourMethods

    void Update()
    {
        if (GameController.gameStarted)
        {
            if (Vector3.Distance(this.transform.position, GameController.purposeLocation) > offset)
            {
                RunToTarget(GameController.purposeLocation);
            }
            else
            {
                Idle();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EnemyCube"))
        {
            enemyCube = collision.collider.gameObject;
            Surround();
        }
    }
    #endregion MonoBehaviourMethods

    public void RunToTarget(Vector3 pos)
    {
        currentSpeed = 5;
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
        transform.LookAt(pos);

        positionHistory.Insert(0, transform.position);
        int index = 0;

        foreach (var bodyParts in body)
        {
            Vector3 point = positionHistory[Mathf.Clamp(index * gap, 0, positionHistory.Count - 1)];

            Vector3 moveDirection = point - bodyParts.transform.position;
            bodyParts.transform.position += moveDirection * currentSpeed * Time.deltaTime;

            bodyParts.transform.LookAt(point);

            index++;
        }
    }

    private void Surround()
    {
        if (surrounded)
        {
            return;
        }

        float angle = 360 / body.Count;
        this.GetComponent<Rigidbody>().isKinematic = true;
        for (int i = 0; i < body.Count; i++)
        {
            body[i].transform.RotateAround(enemyCube.transform.position, Vector3.up, angle * i );
        }
        surrounded = true;
        enemyCube.GetComponent<EnemyCube>().isSurrounded = true;
    }

    public void MakeItStraight()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        foreach (var bodyParts in body)
        {
            int index = 0;
            positionHistory.Insert(0, transform.position);
            Vector3 point = positionHistory[Mathf.Clamp(index * gap, 0, positionHistory.Count - 1)];

            Vector3 moveDirection = point - bodyParts.transform.position;
            bodyParts.transform.position += moveDirection * speed * Time.deltaTime;

            bodyParts.transform.LookAt(point);

            index++;
        }
    }
    public void Idle()
    {
        currentSpeed = 0;
    }
    #region Public Methods

    #endregion


    #region Private Methods

    private void GenerateSnakeBody()
    {
        GameObject bodyObj = Instantiate(bodyPrefabs, body[body.Count - 1].transform.position, Quaternion.identity);
        body.Add(bodyObj);
    }
    #endregion Private Methods

}
