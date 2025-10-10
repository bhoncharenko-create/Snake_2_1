using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isDead = false;

    [Header("Pause Settings")]
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;
    public Button soundButton;
    public Button exitButton;

    [Header("Sound Settings")]
    public GameObject soundSettingsPanel;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Button closeSoundSettingsButton;
    public AudioSource backgroundMusic;

    private bool isPaused = false;
    private bool isSoundEnabled = true;

    private void Awake()
    {
        isDead = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Кнопки паузы
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (soundButton != null)
            soundButton.onClick.AddListener(OpenSoundSettings);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);

        // Кнопки звука
        if (closeSoundSettingsButton != null)
            closeSoundSettingsButton.onClick.AddListener(CloseSoundSettings);

        // Слайдеры
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Панели
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(false);
    }

    private void Update()
    {
        // 👉 Рестарт по пробелу работает ТОЛЬКО если змея мертва
        if (isDead && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
            return;
        }

        // ⛔ Блокируем паузу, если змея мертва
        if (!isDead && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }


    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (backgroundMusic != null)
            backgroundMusic.Pause();

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (backgroundMusic != null)
            backgroundMusic.Play();

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(false);

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // чтобы работало в редакторе Unity
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenSoundSettings()
    {
        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(true);
    }

    public void CloseSoundSettings()
    {
        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(false);
    }

    public void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        AudioListener.volume = isSoundEnabled ? 1f : 0f;

        if (soundButton != null)
        {
            Text buttonText = soundButton.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.text = isSoundEnabled ? "Звук: ВКЛ" : "Звук: ВЫКЛ";
        }
    }

    public void SetMusicVolume(float volume)
    {
        
    }

    public void SetSFXVolume(float volume)
    {
        // сюда можно добавить управление громкостью эффектов
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }
}
