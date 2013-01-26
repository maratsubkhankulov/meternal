using System.Diagnostics;
using Debug = UnityEngine.Debug;
    
public class DebugUtils
{
    public const bool DebugNormal = true;
    public const bool DebugBTrees = false;

    public static void Assert(bool condition)
    {
        if (DebugNormal)
        {
            if (!condition)
            {
                Debug.LogError("Assert failed, see Unity console trace.");
                Debug.Break();
            }
        }
    }

    public static void Log(object output)
    {
        if (DebugNormal)
        {
            Debug.Log(output);
        }
    }

    public static void LogMethod()
    {
        if (DebugNormal)
        {
            StackTrace callStack = new StackTrace();
            StackFrame frame = callStack.GetFrame(1);
            Debug.Log(frame.GetMethod().Name);
        }
    }
}