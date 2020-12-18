using UnityEngine;
public static class TransformExtentions
{
    public static bool IsSiblingOf(this Transform transform, Transform sibling)
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) != transform && transform.parent.GetChild(i) == sibling)
            {
                return true;
            }
        }

        return false;
    }
}