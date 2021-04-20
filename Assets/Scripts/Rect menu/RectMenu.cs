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
        if (Inventory.Instance != null)
        {
            Inventory.Instance.gameObject.SetActive(true);
        }
        if(Parent.IsActive || Parent is StorageObject || Parent is Lever)
        {
            LookButton.gameObject.SetActive(false);
        }

        if(Parent is PromptObject)
        {
            InteractionButton.gameObject.SetActive(false);
        }
        EmergensAnimation.Play();
        FexedPosition();
    }

    private void Update()
    {
        if (GetToCursorDistance() > 5)
            Destroy(gameObject);

    }
    private void OnDestroy()
    {
        Parent.Collider.enabled = true;

        //if(Inventory.Instance != null)
        //    Inventory.Instance.gameObject.SetActive(false);
    }
    private float GetToCursorDistance()
    {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Vector2.Distance(MousePosition, transform.position);
    }
    private void FexedPosition()
    {
        Vector2 cameraPosition = FindObjectOfType<Camera>().transform.position;
        Vector2 directionToCamera = cameraPosition - (Vector2)transform.position;
        Vector2 position = transform.position;
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        directionToCamera /= directionToCamera.magnitude;
        transform.position += (Vector3)directionToCamera * 3;
        //if(Mathf.Abs(cameraPosition.x - transform.position.x) < 10)
        //{

        //    position.x += directionToCamera.x * 10;
        //}
        //if(Mathf.Abs(cameraPosition.y - transform.position.y) < 10)
        //{
        //    position.y += directionToCamera.y * 10; 
        //}
        //transform.position = position;
    }
}
