using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Holes = new List<GameObject>();
    
    [SerializeField]
    private List<GameObject> Rabbits = new List<GameObject>();

    private float waitTime = 1.0f;
    
    private int score;
    public int lives;
    public TMP_Text scoreTxt;
    public TMP_Text livesTxt;

    // Start is called before the first frame update
    private void Awake()
    {
        InvokeRepeating(nameof(SpawnSnowman), waitTime, 1.0f);
        lives += 5;
    }

    // Update is called once per frame
    private void Update()
    {
        GameOver();
        HitRabbit();
        livesTxt.text = lives.ToString();
    }

    // Main function for picking an random position
    private static T PickRandomItem<T>(IReadOnlyList<T> list) {
        var randomNum = Random.Range(0, list.Count);
        return list[randomNum]; 
    }

    // Spawn an random rabbit at an random hole
    private void SpawnSnowman()
    {
        var randomSnowman = PickRandomItem(Rabbits);
        var randomHole = PickRandomItem(Holes);

        var randomHolePos = randomHole.transform.position;
        
        Instantiate(randomSnowman, randomHolePos + new Vector3(0, -1f, 0), Quaternion.AngleAxis(150, Vector3.up));
    }
    
    // Detect when you hit the rabbit
    private void HitRabbit()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Input.GetMouseButtonDown(0)) return;
            if (!Physics.Raycast(ray, out var hit)) return;
                if (!hit.transform.CompareTag("snowman")) return;
                
                    // Add score++
                    score++;
                    scoreTxt.text = score.ToString();
                    
                    Destroy(hit.transform.gameObject);
                    
                    // Play got hit animation
                    // var rabbit = hit.transform.GetComponent<RabbitScript>();
                    // rabbit.anim.play("die")
    }

    private void GameOver()
    {
        if (lives == 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
