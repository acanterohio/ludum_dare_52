using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    protected int slot;
    protected bool hovering;
    protected bool dragging;
    public bool itemExists;
    [SerializeField] protected GameObject highlight;
    [SerializeField] protected Image itemImage;
    [SerializeField] protected GameObject qualityBar;
    [SerializeField] protected Image qualityFill;
    [SerializeField] public Image background;
    [SerializeField] public Color redColor = new Color(.8f, 0, 0, .5f);
    private Color backgroundColor;
    protected InventoryManager manager;

    void Awake()
    {
        if (background == null) background = transform.Find("Background").GetComponent<Image>();
        backgroundColor = background.color;
        manager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        OnStart();
    }


    public virtual void OnStart()
    {
        int rowNumber = transform.parent.GetSiblingIndex();
        int columnNumber = transform.GetSiblingIndex();
        slot = columnNumber + 6 * rowNumber;
    }


    public virtual void UpdateSlot()
    {
        Item item = Inventory.Instance.GetItem(slot);
        UpdateSlotWithItem(item);
    }


    public void StartBlinkingRed()
    {
        StartCoroutine(BlinkRed());
    }

    private IEnumerator BlinkRed()
    {
        Color startColor = backgroundColor;
        Color targetColor = redColor;
        float t = 0;
        float interval = .5f;
        while (!itemExists)
        {
            background.color = Color.Lerp(startColor, targetColor, t / interval);
            t += Time.deltaTime;
            if (t >= interval)
            {
                Color temp = targetColor;
                targetColor = startColor;
                startColor = temp;
                t = 0;
            }
            yield return null;
        }
        background.color = backgroundColor;
    }


    protected virtual void UpdateSlotWithItem(Item item)
    {
        if (manager == null)
        {
            manager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            OnStart();
        }
        if (item == null)
        {
            itemImage.gameObject.SetActive(false);
            qualityBar.SetActive(false);
            itemExists = false;
        }
        else
        {
            Sprite sprite = manager.GetSprite(item.GetType());
            if (sprite == null)
            {
                itemImage.gameObject.SetActive(false);
            }
            else
            {
                itemImage.sprite = sprite;
                itemImage.gameObject.SetActive(true);
            }
            if (item is Organ organ)
            {
                qualityFill.fillAmount = organ.Quality;
                qualityBar.SetActive(true);
            }
            else
            {
                qualityBar.SetActive(false);
            }
            itemExists = true;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        highlight.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (hovering)
        {
            dragging = true;
            StartCoroutine(Drag());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragging)
        {
            itemImage.transform.localPosition = Vector3.zero;
            DropItem();
        }
        dragging = false;
    }

    public virtual void DropItem()
    {
        manager.DropItem(slot);
    }

    private IEnumerator Drag()
    {
        while (dragging)
        {
            Vector3 mousePos = new Vector3(manager.mousePosition.x, manager.mousePosition.y, -10);
            itemImage.GetComponent<RectTransform>().position = mousePos;
            yield return null;
        }
    }

    public virtual void ItemDropped(int originalSlot)
    {
        if (hovering)
        {
            Inventory.Instance.MoveItem(originalSlot, slot);
        }
    }

    public void BrainDropped()
    {
        if (hovering && (!itemExists || Inventory.Instance.GetItem(slot) is Brain))
        {
            Inventory.Instance.EquipBrain(slot);
        }
    }

    public void EyesDropped()
    {
        if (hovering && (!itemExists || Inventory.Instance.GetItem(slot) is Eyes))
        {
            Inventory.Instance.EquipEyes(slot);
        }
    }

    public void LungsDropped()
    {
        if (hovering && (!itemExists || Inventory.Instance.GetItem(slot) is Lungs))
        {
            Inventory.Instance.EquipLungs(slot);
        }
    }

    public void HeartDropped()
    {
        if (hovering && (slot > 0 && !itemExists || Inventory.Instance.GetItem(slot) is Heart))
        {
            Inventory.Instance.EquipHeart(slot);
        }
    }
}
