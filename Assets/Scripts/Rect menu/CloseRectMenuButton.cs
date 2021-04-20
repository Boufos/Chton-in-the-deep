using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRectMenuButton : MonoBehaviour
{
    
    private void OnMouseUp()
    {
        Destroy(GetComponentInParent<RectMenu>().gameObject);
    }
}
