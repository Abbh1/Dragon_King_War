using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public Transform arrow;
    private Camera miniMapCamera;
    public const int maxSize= 30;
    public const int minSize = 10;
    private int cameraSize = 20;
    private void Start()
    {
        miniMapCamera = GetComponent<Camera>();
        arrow = GameObject.Find("Arrow").transform;
    }
    void Update()
    {
        playerTransform = GameController.instance.GetPlayerTransform();
        transform.position = new Vector3(playerTransform.position.x,transform.position.y,playerTransform.position.z);
        arrow.eulerAngles = new Vector3(0, 0,-playerTransform.eulerAngles.y);
    }
    
    public void ChangeMapSize(int changeSize)
    {
        cameraSize += changeSize;
        cameraSize = Mathf.Clamp(cameraSize, minSize, maxSize);
        miniMapCamera.orthographicSize = cameraSize;

    }
}
