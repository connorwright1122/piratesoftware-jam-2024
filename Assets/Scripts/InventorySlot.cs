using Unity.VisualScripting;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public Image Icon;
    public string ItemGuid = "";

    public InventorySlot()
    {
        //Create a new Image element and add it to the root
        Icon = new Image();
        Add(Icon);

        //Add USS style properties to the elements
        Icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");

        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion

    public void HoldItem(ItemDetails item)
    {
        Icon.image = item.Icon.texture;
        ItemGuid = item.GUID;
    }

    public void DropItem()
    {
        ItemGuid = "";
        Icon.image = null;
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button
        if (evt.button != 0 || ItemGuid.Equals(""))
        {
            return;
        }

        //Clear the image
        Icon.image = null;

        //Start the drag
        InventoryUIController.StartDrag(evt.position, this);
    }
}