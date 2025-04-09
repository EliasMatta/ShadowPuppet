using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f; // Time for fading

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep FadeManager across levels
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (fadePanel == null)
        {
            Debug.LogError("FadeManager: FadePanel is not assigned in the Inspector!");
            return;
        }

        // START OF THE LEVEL: DO NOTHING (NO BLACK SCREEN)
        fadePanel.alpha = 0; // Make sure it is fully visible at the start
    }

    public void FadeToNextLevel()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Debug.Log("Fading out...");
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadePanel.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadePanel.alpha = 1f;
        Debug.Log("Fade-out complete! Loading next scene...");

        yield return StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int nextLevelNumber = int.Parse(currentScene.Replace("Level", "")) + 1;
        string nextSceneName = "Level" + nextLevelNumber;

        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
            yield return null; // Wait one frame for the new scene to load

            // **Reassign fadePanel in the new scene**
            yield return new WaitForSeconds(0.1f); // Short delay to ensure UI loads
            GameObject newFadePanel = GameObject.Find("FadePanel");

            if (newFadePanel != null)
            {
                fadePanel = newFadePanel.GetComponent<CanvasGroup>();
                StartCoroutine(FadeIn()); // Fade in after loading
            }
            else
            {
                Debug.LogError("FadeManager: No FadePanel found in the new scene!");
            }
        }
        else
        {
            Debug.Log("No more levels! Game finished.");
        }
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("Fading in...");
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            if (fadePanel == null) yield break; // Prevent errors if fadePanel is missing
            fadePanel.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (fadePanel != null) fadePanel.alpha = 0f; // Fully transparent
        Debug.Log("Fade-in complete!");
    }
}
