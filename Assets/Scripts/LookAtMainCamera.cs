using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    public Transform pivot;
    
    // Update is called once per frame
    void Update()
    {
        pivot.LookAt(Camera.main.transform.position);
    }
}
