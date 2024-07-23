using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    //public List<InventorySlot> InventoryItems = new List<InventorySlot>();
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();
    public List<VisualElement> CraftingItems = new List<VisualElement>();
    public List<InventorySlot> FlaskItems = new List<InventorySlot>();

    private VisualElement m_Root;
    private VisualElement m_SlotContainer;
    private VisualElement m_DroppableContainer;
    private VisualElement m_CraftingContainer;

    private static VisualElement m_GhostIcon;

    private static bool m_IsDragging;
    private static InventorySlot m_OriginalSlot;
    private static ElementSlot m_OriginalESlot;

    private void Awake()
    {
        //Store the root from the UI Document component
        m_Root = GetComponent<UIDocument>().rootVisualElement;

        //Search the root for the SlotContainer Visual Element
        m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");
        m_DroppableContainer = m_Root.Q<VisualElement>("DroppableContainer");
        m_CraftingContainer = m_Root.Q<VisualElement>("CraftingContainer");

        //Create InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < 14; i++)
        {
            //InventorySlot item = new InventorySlot();
            InventorySlot item = new InventorySlot();

            InventoryItems.Add(item);

            m_SlotContainer.Add(item);
        }

        //CraftingItems = m_CraftingContainer.Q<InventorySlot>;
        //CraftingItems = m_Root.Query<VisualElement>("CraftingContainer").Children<VisualElement>("InventorySlot").ToList();
        //CraftingItems = m_CraftingContainer.Query("InventorySlot").ToList();
        //CraftingItems = m_CraftingContainer.Query<VisualElement>().ToList();
        CraftingItems = m_CraftingContainer.Query<VisualElement>(className: "slotContainer").ToList();
        // Log the number of crafting items found to help debug
        Debug.Log("Number of Crafting Items found: " + CraftingItems.Count);


        GameController.OnInventoryChanged += GameController_OnInventoryChanged;
        m_GhostIcon = m_Root.Query<VisualElement>("GhostIcon");

        m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void GameController_OnInventoryChanged(string[] itemGuid, InventoryChangeType change)
    {
        //Loop through each item and if it has been picked up, add it to the next empty slot
        foreach (string item in itemGuid)
        {
            if (change == InventoryChangeType.Pickup)
            {
                var emptySlot = InventoryItems.FirstOrDefault(x => x.ItemGuid.Equals(""));

                if (emptySlot != null)
                {
                    emptySlot.HoldItem(GameController.GetItemByGuid(item));
                }
            }
        }
    }

    public static void StartDrag(Vector2 position, InventorySlot originalSlot)
    {
        //Set tracking variables
        m_IsDragging = true;
        m_OriginalSlot = originalSlot;

        //Set the new position
        m_GhostIcon.style.top = position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = position.x - m_GhostIcon.layout.width / 2;

        //Set the image
        m_GhostIcon.style.backgroundImage = GameController.GetItemByGuid(originalSlot.ItemGuid)
          .Icon.texture;

        //Flip the visibility on
        m_GhostIcon.style.visibility = Visibility.Visible;
    }

    public static void StartDrag(Vector2 position, ElementSlot originalSlot)
    {
        //Set tracking variables
        m_IsDragging = true;
        m_OriginalESlot = originalSlot;

        //Set the new position
        m_GhostIcon.style.top = position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = position.x - m_GhostIcon.layout.width / 2;

        //Set the image
        m_GhostIcon.style.backgroundImage = GameController.GetItemByGuid(originalSlot.ItemGuid)
          .Icon.texture;

        //Flip the visibility on
        m_GhostIcon.style.visibility = Visibility.Visible;
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        //Only take action if the player is dragging an item around the screen
        if (!m_IsDragging)
        {
            return;
        }

        //Set the new position
        m_GhostIcon.style.top = evt.position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = evt.position.x - m_GhostIcon.layout.width / 2;

    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!m_IsDragging)
        {
            return;
        }

        if (m_DroppableContainer.worldBound.Overlaps(m_GhostIcon.worldBound))
        {

            //Check to see if they are dropping the ghost icon over any inventory slots.
            //IEnumerable<InventorySlot> slots = InventoryItems.Where(x =>
            //   x.worldBound.Overlaps(m_GhostIcon.worldBound));
            IEnumerable<VisualElement> slots = CraftingItems.Where(x =>
               x.worldBound.Overlaps(m_GhostIcon.worldBound));
            //Debug.Log(CraftingItems.Count());
            //Debug.Log("Placed in crafting");
            //Found at least one
            if (slots.Count() != 0)
            {
                InventorySlot closestSlot = (InventorySlot) slots.OrderBy(x => Vector2.Distance
                   (x.worldBound.position, m_GhostIcon.worldBound.position)).First();

                //Set the new inventory slot with the data
                closestSlot.HoldItem(GameController.GetItemByGuid(m_OriginalSlot.ItemGuid));

                //Clear the original slot
                m_OriginalSlot.DropItem();

                Debug.Log("Placed in crafting");
            }
        }
        //Didn't find any (dragged off the window)
        else
        {
            Debug.Log("Not crafting");
            m_OriginalSlot.Icon.image =
                  GameController.GetItemByGuid(m_OriginalSlot.ItemGuid).Icon.texture;
        }

        //Clear dragging related visuals and data
        m_IsDragging = false;
        m_OriginalSlot = null;
        m_GhostIcon.style.visibility = Visibility.Hidden;

    }
}
