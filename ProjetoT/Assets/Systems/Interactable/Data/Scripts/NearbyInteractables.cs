using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NearbyInteractables : MonoBehaviour
{
    private List<Interactable> _NearbyInteractables = new List<Interactable>();
    public static Interactable ClosestInteractable;
    public static string InteractableName = "Interactable";

    public Transform InstigatorPoint;

    private void Start()
    {
        Messenger.AddListener<string>(InteractableName, UpdateList);
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable inter = other.GetComponent<Interactable>();
        if(inter != null && !_NearbyInteractables.Contains(inter))
        {
            _NearbyInteractables.Add(inter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable inter = other.GetComponent<Interactable>();
        if(inter != null)
        {
            inter.SetIsClose(false);
            _NearbyInteractables.Remove(inter);
        }
    }

    private Interactable GetClosestInteractable()
    {
        if (_NearbyInteractables.Count == 0)
            return null;

        Interactable closest = _NearbyInteractables.OrderBy(i => Vector3.Distance(i.transform.position, InstigatorPoint.position)).FirstOrDefault();
        return closest;
    }

    private void UpdateList(string itemName)
    {
        Interactable interactable = _NearbyInteractables.Find(i => i.ItemName == itemName);
        if (interactable != null && !interactable.gameObject.activeSelf)
        {
            interactable.SetIsClose(false);
            _NearbyInteractables.Remove(interactable);
        }
    }

    private void UpdateTipBoxVisible()
    {
        if (_NearbyInteractables.Count == 0)
            return;

        _NearbyInteractables.ForEach(i => i.SetIsClose(i == ClosestInteractable));
    }

    private void Update()
    {
        Interactable closest = GetClosestInteractable();

        ClosestInteractable = closest;
        UpdateTipBoxVisible();
    }

    //public static NearbyInteractables Instance;

    //[SerializeField] private Transform m_InstigatorPoint;
    //private List<Interactable> m_Interactables = new List<Interactable>();

    //private void Awake()
    //{
    //    Instance = this;
    //    PlayerInputManager.Instance.PlayerInput.World.Action.performed += DoInteraction;
    //}

    //private void OnDestroy()
    //{
    //    PlayerInputManager.Instance.PlayerInput.World.Action.performed -= DoInteraction;
    //}

    //private void Update()
    //{
    //    if (HasNearbyInteractables())
    //    {
    //        UpdateVisibleTipBox();
    //    }
    //}

    //public void DoInteraction(InputAction.CallbackContext context)
    //{
    //    if (!HasNearbyInteractables() && GameStateManager.Game.State == GameState.World_Free)
    //        return;

    //    ClosestInteractables().DoInteraction();
    //}

    //#region ListManipulation
    //public void AddInteractable(Interactable item)
    //{
    //    m_Interactables.Add(item);
    //}

    //public void RemoveInteractable(Interactable item)
    //{
    //    item.SetIsClose(false);
    //    m_Interactables.Remove(item);
    //}

    //public bool AlreadyInTheList(Interactable item)
    //{
    //    for (int i = 0; i < m_Interactables.Count; i++)
    //    {
    //        if(m_Interactables[i] == item)
    //        {
    //            if (m_Interactables[i].ItemID == item.ItemID)
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                Debug.Log("O item <" + m_Interactables[i].name + "> está com ID igual a de outro item.");
    //            }
    //        }
    //    }

    //    return false;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Interactable interactable = other.GetComponent<Interactable>();

    //    if (interactable != null)
    //    {
    //        if (interactable.CanInteract && !AlreadyInTheList(interactable))
    //        {
    //            AddInteractable(interactable);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    Interactable interactable = other.GetComponent<Interactable>();

    //    if (interactable != null && AlreadyInTheList(interactable))
    //    {
    //        RemoveInteractable(interactable);
    //    }
    //}
    //#endregion

    //#region Utilities
    //private void UpdateVisibleTipBox()
    //{
    //    if (m_Interactables.Count == 0)
    //        return;

    //    for (int i = 0; i < m_Interactables.Count; i++)
    //    {
    //        if (m_Interactables[i] == ClosestInteractables())
    //        {
    //            m_Interactables[i].SetIsClose(true);
    //        }
    //        else
    //        {
    //            m_Interactables[i].SetIsClose(false);
    //        }
    //    }
    //}

    //public bool HasNearbyInteractables()
    //{
    //    return m_Interactables.Count != 0;
    //}

    //public Interactable ClosestInteractables()
    //{
    //    int closestIndex = 0;

    //    for (int i = 0; i < m_Interactables.Count; i++)
    //    {
    //        var atualClosest = Vector3.Distance(m_Interactables[closestIndex].transform.position, transform.position);
    //        var newCheackage = Vector3.Distance(m_Interactables[i].transform.position, transform.position);

    //        if (newCheackage < atualClosest)
    //        {
    //            closestIndex = i;
    //        }
    //    }

    //    return m_Interactables[closestIndex];
    //}
    //#endregion
}