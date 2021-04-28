using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectMenu : MonoBehaviour
{
    public LookButton LookButton;
    public InteractoinButton InteractionButton;
    private Animation EmergensAnimation;
    public InteractableObject Parent;
    public float MaxDistanceToCursor;
    public static RectMenu Instance;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        InteractionButton = GetComponentInChildren<InteractoinButton>();
        LookButton = GetComponentInChildren<LookButton>();
        EmergensAnimation = GetComponent<Animation>();
        EmergensAnimation.Play();
        FexedPosition();
    }

    private void Update()
    {
        if (GetToCursorDistance() > 5)
            Destroy(gameObject);
    }
    private float GetToCursorDistance()
    {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Vector2.Distance(MousePosition, transform.position);
    }
    private void FexedPosition()
    {
        Vector3 cameraPosition = FindObjectOfType<Camera>().transform.position;
        Vector3 directionToCamera = cameraPosition - transform.position;
        Vector3 position = transform.position;
        directionToCamera /= directionToCamera.magnitude;
        position = transform.position + directionToCamera * 3;
        position.z = cameraPosition.z - directionToCamera.z * 3;
        transform.position = position;
    }
}
