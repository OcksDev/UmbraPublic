using UnityEngine;

public static class Clipb
{
    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    public static string GetClipboard()
    {
        return GUIUtility.systemCopyBuffer;
    }
}
