using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerDuration = 30f;
    private float timer;
    private Text timerText;

    void Start()
    {
        timer = timerDuration;

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas", typeof(Canvas));
            canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        GameObject textGO = new GameObject("TimerText", typeof(Text));
        textGO.transform.SetParent(canvas.transform);

        timerText = textGO.GetComponent<Text>();

        RectTransform rt = textGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 1f);  // Top center
        rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0.5f, 1f);
        rt.anchoredPosition = new Vector2(0, -50);  // Slightly below top
        rt.sizeDelta = new Vector2(400, 100);

        // Set up Text component
        timerText.alignment = TextAnchor.MiddleCenter;
        timerText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        timerText.fontSize = 36;
        timerText.color = Color.white;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(0f, timer);

        if (timerText != null)
        {
            timerText.text = "Timer: " + timer.ToString("F1"); // 1 decimal
        }

        if (timer <= 0f)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}