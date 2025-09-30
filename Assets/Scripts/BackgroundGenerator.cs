using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
public GameObject backgroundPrefab;
    private void Start()
    {bool a = false;
        for (int i = -SnakeController.Instance.fieldWidth; i <= SnakeController.Instance.fieldWidth; i++)
        {
            for (int j = -SnakeController.Instance.fieldHeight; j <= SnakeController.Instance.fieldHeight; j++)
            {
                a = !a;
                if (a) continue;

                Instantiate(backgroundPrefab, new Vector3(i, j, 0), Quaternion.identity).transform.parent=transform;
            }
        }
    }
}
