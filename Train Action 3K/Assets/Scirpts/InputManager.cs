using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ApplyToAllAtMouse(delegate (Collider2D collider)
            {
                InvokeOnScripts(collider.gameObject, "OnMouseDownAll");

                System.Reflection.MethodInfo m = collider.gameObject.GetType().GetMethod("OnMouseDownAll");
            });
        }
        if (Input.GetMouseButtonUp(0))
        {
            ApplyToAllAtMouse(delegate (Collider2D collider)
            {
                InvokeOnScripts(collider.gameObject, "OnMouseUpAll");

            });
        }
    }

    void ApplyToAllAtMouse(Action<Collider2D> action)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        foreach (Collider2D hit in hits)
        {
            action(hit);
        }
    }

    void InvokeOnScripts(GameObject gameObject, String methodname)
    {
        MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();
        foreach (Component c in components)
        {
            System.Reflection.MethodInfo m = c.GetType().GetMethod(methodname);
            if (m != null)
            {
                m.Invoke(c, 0, null, null, null);
            }
        }

    }
}
