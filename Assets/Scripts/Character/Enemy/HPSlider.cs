using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    private Camera mainCamera;
    private Canvas canvas;
    private Slider slider;
    private GameObject enemy;
    // Update is called once per frame
    private void Start()
    {
        enemy = transform.parent.gameObject;
        mainCamera = Camera.main;
        slider = GetComponentInChildren<Slider>();
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = mainCamera;
    }
    void Update()
    {
        transform.LookAt(mainCamera.transform.position);
        slider.value = enemy.GetComponent<State>().value.presentHP / enemy.GetComponent<State>().value.maxHP;
    }
}
