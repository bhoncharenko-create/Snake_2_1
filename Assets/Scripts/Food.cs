using UnityEngine;

public class Food : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == SnakeController.Instance.snakeParts[0].transform)
        {
            Score.Instance.IncreaseScore(1);
            SnakeController.Instance.Grow();
            FoodSpawner.Instance.SpawnFood();
            Destroy(gameObject);
        }
    }

}
