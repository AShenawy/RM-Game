using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [TextArea]
    public string objectDescription;
    public bool canPickUp;

    private Collider2D objectCollider;
            
    private void Start()
    {
        objectCollider = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        Vector2 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (objectCollider.OverlapPoint(mouseLocation))
        {
            GameManager.gm.canInteract = true;
            GameManager.gm.interactableObject = this;
        }
        else
        {
            GameManager.gm.canInteract = false;
            GameManager.gm.interactableObject = null;
        }
    }

    public void Inspect()
    {
        print("Inspecting " + name);
    }
    
    public void Interact()
    {
        print("Interacting with " + name);
    }

    public void PickUp()
    {
        if (canPickUp)
        {
            print("Picked up " + name);
            
        }
        else
        {
            print("Cannot pick up " + name);
        }
            
    }
}