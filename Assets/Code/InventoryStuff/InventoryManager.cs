using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    private class NamedSprite
    {
        public string name;
        public Sprite sprite;

        public static bool Contains(List<NamedSprite> list, string name)
        {
            foreach (NamedSprite s in list)
            {
                if (s.name == name) return true;
            }
            return false;
        }

        public static Sprite GetSprite(List<NamedSprite> list, string name)
        {
            foreach (NamedSprite s in list)
            {
                if (s.name == name) return s.sprite;
            }
            return null;
        }
    }

    [SerializeField] private GameObject inventoryParent;
    [SerializeField] private Transform rowsParent;
    [SerializeField] private List<NamedSprite> sprites;
    [SerializeField] private List<InventorySlot> organSlots;
    [SerializeField] private RectTransform currentOrgans;
    [SerializeField] private Vector3 sidebarPos, sidebarInvPos;
    [SerializeField] private Vector3 sidebarScale, sidebarInvScale;
    [SerializeField] private Phone phone;
    [SerializeField] private GameObject phoneParent;
    public Camera cam;
    public Vector2 mousePosition;
    private bool showingInventory;

    void Start()
    {
        UpdateAllSlots();
        currentOrgans.anchoredPosition = sidebarPos;
        currentOrgans.localScale = sidebarScale;
        // StartCoroutine(TestInventory());
        Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator TestInventory()
    {
        while (true)
        {
            Inventory.Instance.AddItem(RandomOrgan());
            Inventory.Instance.AddItem(new NormalAmmo());
            UpdateAllSlots();
            yield return new WaitForSeconds(5f);
        }
    }

    private Organ RandomOrgan()
    {
        int rand = Random.Range(0, 4);
        if (rand == 0) return new Brain();
        if (rand == 1) return new Eyes();
        if (rand == 2) return new Lungs();
        return new Heart();
    }


    public void GetMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }


    public void ToggleInventory(InputAction.CallbackContext context)
    {
        showingInventory = !showingInventory;
        if (showingInventory)
        {
            Cursor.lockState = CursorLockMode.None;
            currentOrgans.anchoredPosition = sidebarInvPos;
            currentOrgans.localScale = sidebarInvScale;
            phoneParent.SetActive(true);
            UpdateAllSlots();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            currentOrgans.anchoredPosition = sidebarPos;
            currentOrgans.localScale = sidebarScale;
            phoneParent.SetActive(false);
        }
        inventoryParent.SetActive(showingInventory);
    }


    public Sprite GetSprite(System.Type type)
    {
        string name = type.Name;
        Sprite sprite = NamedSprite.GetSprite(sprites, name);
        if (sprite == null) Debug.LogError("Type " + name + " does not exist in the InventoryManager");
        return sprite;
    }

    public void DropItem(int slot)
    {
        foreach (Transform row in rowsParent)
        {
            foreach (Transform itemSlot in row)
            {
                itemSlot.GetComponent<InventorySlot>().ItemDropped(slot);
            }
        }
        foreach (InventorySlot organSlot in organSlots)
        {
            organSlot.ItemDropped(slot);
        }
        phone.ItemDropped(slot);
        UpdateAllSlots();
    }

    public void DropBrain()
    {
        foreach (Transform row in rowsParent)
        {
            foreach (Transform itemSlot in row)
            {
                itemSlot.GetComponent<InventorySlot>().BrainDropped();
            }
        }
        UpdateAllSlots();
    }

    public void DropEyes()
    {
        foreach (Transform row in rowsParent)
        {
            foreach (Transform itemSlot in row)
            {
                itemSlot.GetComponent<InventorySlot>().EyesDropped();
            }
        }
        UpdateAllSlots();
    }

    public void DropLungs()
    {
        foreach (Transform row in rowsParent)
        {
            foreach (Transform itemSlot in row)
            {
                itemSlot.GetComponent<InventorySlot>().LungsDropped();
            }
        }
        UpdateAllSlots();
    }

    public void DropHeart()
    {
        foreach (Transform row in rowsParent)
        {
            foreach (Transform itemSlot in row)
            {
                itemSlot.GetComponent<InventorySlot>().HeartDropped();
            }
        }
        UpdateAllSlots();
    }

    public void UpdateAllSlots()
    {
        foreach (Transform row in rowsParent)
        {
            foreach (Transform itemSlot in row)
            {
                itemSlot.GetComponent<InventorySlot>().UpdateSlot();
            }
        }
        foreach (InventorySlot organSlot in organSlots)
        {
            organSlot.UpdateSlot();
        }
    }
}
