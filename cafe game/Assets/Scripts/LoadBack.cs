using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private void Update()
    {
        // Wait one frame so the loading scene can render
        Loader.LoaderCallback();
    }
}
