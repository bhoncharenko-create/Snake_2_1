using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pause Settings")]
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;
    public Button soundButton;

    [Header("Sound Settings")]
    public GameObject soundSettingsPanel;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Button closeSoundSettingsButton;

    private bool isPaused = false;
    private bool isSoundEnabled = true;

    private void Awake()
    {
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
        // Настраиваем кнопки паузы
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (soundButton != null)
            soundButton.onClick.AddListener(OpenSoundSettings);

        // Настраиваем кнопки настроек звука
        if (closeSoundSettingsButton != null)
            closeSoundSettingsButton.onClick.AddListener(CloseSoundSettings);

        // Настраиваем слайдеры
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Скрываем панели в начале
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(false);
    }

    private void Update()
    {
        // Пауза по ESC
        if (Input.GetKeyDown(KeyCode.Escape))
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

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(false);

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        // Снимаем паузу и перезагружаем сцену
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSoundSettings()
    {
        // Открываем панель настроек звука
        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(true);
    }

    public void CloseSoundSettings()
    {
        // Закрываем панель настроек звука
        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(false);
    }

    public void ToggleSound()
    {
        // Переключаем звук
        isSoundEnabled = !isSoundEnabled;
        AudioListener.volume = isSoundEnabled ? 1f : 0f;

        // Обновляем текст кнопки
        if (soundButton != null)
        {
            Text buttonText = soundButton.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.text = isSoundEnabled ? "Звук: ВКЛ" : "Звук: ВЫКЛ";
        }
    }

    public void SetMusicVolume(float volume)
    {
        // Устанавливаем громкость музыки
        // Здесь нужно заменить на твою систему аудио
        // Пример: AudioManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        // Устанавливаем громкость звуковых эффектов
        // Здесь нужно заменить на твою систему аудио
        // Пример: AudioManager.Instance.SetSFXVolume(volume);
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }
}