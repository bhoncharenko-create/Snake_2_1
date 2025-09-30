using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public GameObject deathMenu;
    public AudioSource crashSound;
    public GameObject pauseButton;
    public void Start()
    {
        Time.timeScale = 1;
        deathMenu.SetActive(false);
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var snakePart = collision.GetComponent<SnakeMovPart>();
        if ((snakePart != null && snakePart != SnakeController.Instance.snakeParts[1]) ||collision.gameObject.name=="Wall")
        {
            crashSound.Play();  
            deathMenu.SetActive(true); 
            Time.timeScale = 0;
            pauseButton.SetActive(false);
            GameManager.isDead = true;
        }
    }
}
