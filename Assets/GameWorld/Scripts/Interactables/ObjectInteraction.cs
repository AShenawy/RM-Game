using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [TextArea]
    public string objectDescription;
    public bool canPickUp;

    //private Collider2D objectCollider;
    private Collider objectCollider;
            
    private void Start()
    {
        //objectCollider = GetComponent<Collider2D>();
        objectCollider = GetComponent<Collider>();
    }
    
    private void Update()
    {
        // Vector2 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // if (objectCollider.bounds.Contains(mouseRay.direction))
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider == objectCollider)
            {
                GameManager.instance.canInteract = true;
                GameManager.instance.interactableObject = this; 
            }
        }
        else
        {
            GameManager.instance.canInteract = false;
            GameManager.instance.interactableObject = null;
        }
    }

    public virtual string InspectObject()
    {
        print("Inspecting " + name);
        
        return objectDescription;
    }
    
    public virtual void InteractWithObject()
    {
        print("Interacting with " + name);
    }

    public virtual void PickUpObject()
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