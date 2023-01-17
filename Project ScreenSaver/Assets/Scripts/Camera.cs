using UnityEngine;

public class Camera : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, -10);
    }
}
