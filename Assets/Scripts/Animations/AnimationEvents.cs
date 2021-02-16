using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void HideGameObject()
    {
        gameObject.SetActive(false);
    }
}
