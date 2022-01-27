using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region MemberFields
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text cubeCounter;
    [SerializeField]
    private Image mapImage;
    [SerializeField]
    private GameObject darkPanel;

    private int allBox;
    private float boxFillRate;
    private float currentDestroyedBox = 0;
    private Snake snake;
    private bool isFinished = false;
    public static Vector3 purposeLocation = Vector3.zero;
    public static bool gameStarted = false;
    public static int Score { get; set; }
    #endregion MemberFields


    #region MonoBehaviourMethods

    private void Awake()
    {
        darkPanel.SetActive(false);
        allBox = FindObjectsOfType<EnemyCube>().Length;
        cubeCounter.text = string.Format("{0}", allBox);
        mapImage.fillAmount = currentDestroyedBox;
        boxFillRate = 1.0f / allBox;
        snake = FindObjectOfType<Snake>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !isFinished)
        {
            if (!gameStarted)
            {
                gameStarted = true;
            }
            else
            {
                SetPurposeAndTarget(Input.mousePosition);
            }
        }
    }

    #endregion MonoBehaviourMethods


    #region Public Methods

    public void AddScore(int scr)
    {
        Score += scr;
        currentDestroyedBox += boxFillRate;
        mapImage.fillAmount = currentDestroyedBox;
        allBox -= 1;
        scoreText.text = string.Format("{0}", Score);
        cubeCounter.text = string.Format("{0}", allBox);
        if (allBox == 0)
        {
            isFinished = true;
            darkPanel.SetActive(true);
        }
    }
    public void SetPurposeAndTarget(Vector3 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 temp;
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("EnemyCube"))
            {
                temp = new Vector3(hit.point.x, hit.point.y - .35f, hit.point.z);
                purposeLocation = temp;
            }
            else if (hit.collider.CompareTag("Plane"))
            {
                temp = new Vector3(hit.point.x, hit.point.y + .35f, hit.point.z);
                purposeLocation = temp;
            }
            
        }
    }
    #endregion Public Methods



}
