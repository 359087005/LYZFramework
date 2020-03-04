using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ProgramExtensions : MonoSingleton<ProgramExtensions>
{
   public static GameObject target;
    Coroutine cor;
    public static void Create()
    {
        if (target == null)
        {
            target = new GameObject("[ProgramExtensions]");
            target.AddComponent<ProgramExtensions>();
        }
    }
    public Coroutine WaitForCompletion(float delay, UnityAction action)
    {
        cor = StartCoroutine(ExecutAction(delay, action));
        return cor;
    }
    IEnumerator ExecutAction(float delay, UnityAction action)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
   public void Stop()
    {
        StopAllCoroutines();
    }
}
