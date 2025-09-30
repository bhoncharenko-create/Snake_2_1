using System.Collections;
using System.Threading;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    
    public static FoodSpawner Instance;
    public GameObject foodPrefab;
    public float foodTimer;
    public AudioSource eatSound;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //StartCoroutine(spawntimer()); 
        SpawnFood();
    }
    private IEnumerator spawntimer()
    {
        while (true)
        {
            Vector3 spawnPos;
            do
            {
                 spawnPos = new Vector3(Random.Range(-SnakeController.Instance.fieldWidth, SnakeController.Instance.fieldWidth + 1), Random.Range(-SnakeController.Instance.fieldHeight, SnakeController.Instance.fieldHeight + 1));
            }
            while(!checkspawnpos(spawnPos));
            Instantiate(foodPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(foodTimer);
        }
    }
    public void SpawnFood()
    {
        Vector3 spawnPos;
        do
        {
            spawnPos = new Vector3(Random.Range(-SnakeController.Instance.fieldWidth, SnakeController.Instance.fieldWidth + 1), Random.Range(-SnakeController.Instance.fieldHeight, SnakeController.Instance.fieldHeight + 1));
        }
        while (!checkspawnpos(spawnPos));
        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
        eatSound.Play();
    }
    private bool checkspawnpos(Vector3 pos)
    {
        foreach (var part in SnakeController.Instance.snakeParts)
        {
            if (part.lastPos == (Vector2)pos)
            {
                return false;
            }
        }
        return true;
    }
}
