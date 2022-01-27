using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    [SerializeField, Range(0, 50)]
    private int damageRate = 33;
    [SerializeField, Range(0, 200)]
    private float health = 100;
    [SerializeField, Range(1, 50)]
    private int scoreValue = 5;

    private float scaleRate = 0;
    private float halfScale;
    public bool isSurrounded = false;
    private Snake snake;
    private GameController gameController;
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        snake = FindObjectOfType<Snake>();
        scaleRate = .002f;
        halfScale = this.transform.localScale.x / 2;
    }

    void Update()
    {
        if (isSurrounded)
        {
            health -= damageRate * Time.deltaTime;
            if (this.transform.localScale.x >= halfScale)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x - scaleRate,
                this.transform.localScale.y, this.transform.localScale.z - scaleRate);
            }
            if (health < 35)
            {
                gameController.AddScore(scoreValue);
                snake.surrounded = false;
                snake.enemyCube = null;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        snake.MakeItStraight();
    }
}
