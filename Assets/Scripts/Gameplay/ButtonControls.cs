using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    // sprites for active and inactive states
    public Sprite inactiveSpriteImage;
    public Sprite activeSpriteImage;

    public KeyCode key;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // switch between active and inactive states on key press
        if (Input.GetKeyDown(key))
        {
            spriteRenderer.sprite = activeSpriteImage;
        }

        if (Input.GetKeyUp(key))
        {
            spriteRenderer.sprite = inactiveSpriteImage;
        }
    }
}
