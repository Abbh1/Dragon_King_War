using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    public Text content;
    public TipType type;
    private float speed;
    private float minAlpha;
    private CanvasGroup canvasGroup;
    public enum TipType
    {
        Quick,
        Middle,
        Slow,
        Lasting
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        AssignValueByType();
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, speed * Time.deltaTime);
        if (canvasGroup.alpha < minAlpha)
            Destroy(gameObject);
    }
    private void AssignValueByType()
    {
        switch (type)
        {
            case TipType.Quick: { speed = 4.0f;minAlpha = 0.1f; } break;
            case TipType.Middle: { speed = 3.0f;minAlpha = 0.1f; } break;
            case TipType.Slow: { speed = 2.0f;minAlpha = 0.1f; }break;
            case TipType.Lasting: { speed = 0;minAlpha = 0;}break;
            default:break;
        }
    }

    public void onClicked()
    {
        Destroy(gameObject);
    }
}
