public class GenericUseable : Useable
{
    private void OnEnable()
    {
        SetName = GetSetNameFromParent(transform.parent.gameObject);
    }
}