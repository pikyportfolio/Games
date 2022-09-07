using UnityEngine;
using System.Collections;

public class ChallengeGeneratingScript : MonoBehaviour {
    public float scrollingSpeed = 5.00f;
    public GameObject[] challenges;
    public float frequency = 0.5f;
    float counter = 0.0f;
    public Transform challengesSpawnPoint;
    bool isGameOver = false;

    // Use this for initialization
    void Start() {
        RandomChallengeGenerator();

    }

    // Update is called once per frame
    void Update() {
        if (isGameOver) return; 
        //Random Objects generation
        if (counter <= 0.0f)
        {
            RandomChallengeGenerator();
        }
        else {
            counter -= Time.deltaTime * frequency;
        }

        //Scrolling 
        GameObject currentChild;
        for (int i = 0; i < transform.childCount; i++) {
            currentChild = transform.GetChild(i).gameObject;
            Chanllenge(currentChild);
            //Deleting the object once they are off camera
            if (currentChild.transform.position.x <= -15.0f) { 
                Destroy(currentChild);
            }
        }
    }

    void Chanllenge(GameObject currentChallenge) {
        currentChallenge.transform.position -= Vector3.right * (scrollingSpeed * Time.deltaTime);
    }

    void RandomChallengeGenerator() {
        GameObject newChallenge = Instantiate(challenges[Random.Range(0,challenges.Length)],challengesSpawnPoint.position,Quaternion.identity) as GameObject;
        newChallenge.transform.parent = transform;
        counter = 1.0f;
    }

    public void GameOver() {
        isGameOver = true;
        transform.GetComponent<GameController>().GameOver();
    }
    
}
