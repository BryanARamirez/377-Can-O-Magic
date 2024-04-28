using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [04/28/2024]
 * [shows a sprite of power item gained]
 */

public class PowerItemNotifyScript : MonoBehaviour
{
    [SerializeField] private Sprite _powerItemSprite;
    [SerializeField] private float _spriteSize;

    [SerializeField] private float timeDuration = 1f;
    private float timeStart;
    private bool move = false;

    private SpriteRenderer _spriteRenderer;

    private GameObject _powerItemButton;
    private RectTransform _buttonTransform;
    private Vector3 _startLoc;
    private Vector3 _endLoc;

    /// <summary>
    /// sets the sprite and size
    /// finds location of the power item button
    /// </summary>
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 5);
        _startLoc = transform.position;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = _powerItemSprite;
        transform.localScale = new Vector3(_spriteSize, _spriteSize, 1);

        _powerItemButton = GameObject.FindGameObjectWithTag("PowerItemButton");
        if (_powerItemButton != null)
        {
            StartCoroutine(PauseStartMove());
            _buttonTransform = _powerItemButton.GetComponent<RectTransform>();

            Vector3 screenPoint = _buttonTransform.TransformPoint(_buttonTransform.rect.center);
            _endLoc = Camera.main.ScreenToWorldPoint(screenPoint);
            _endLoc = new Vector3(_endLoc.x, _endLoc.y, 5);
        }
        timeStart = Time.time;
    }

    private void Update()
    {
        if (move)
        {
            float u = (Time.time - timeStart) / timeDuration;

            if (u >= 1)
            {
                move = !move;
                Destroy(gameObject, .1f);
            }

            u = (1 - u) * 0f + u * 1f;
            u = Mathf.Pow(u, 3);

            if (_endLoc != null)
            {
                transform.position = (1 - u) * _startLoc + u * _endLoc;
            }

            Color tmp = _spriteRenderer.color;
            tmp.a = (1 - u);
            _spriteRenderer.color = tmp;
        }
    }

    private IEnumerator PauseStartMove()
    {
        yield return new WaitForSeconds(.5f);
        move = true;
    }
}
