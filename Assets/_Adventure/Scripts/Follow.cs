using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Follow : MonoBehaviour
{
    public float speed = 14;
    Vector3 _velocity = Vector3.zero;
    Vector3 direction;
    private bool updateEnabled = true;
    Vector3 target;

    private void OnEnable()
    {
        updateEnabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (updateEnabled)
        {
            target = GameManager.Instance.Player.transform.position;
            direction = (transform.position - target);
            if (direction.magnitude > 0.6f)
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                    target,
                    ref _velocity,
                    Time.deltaTime * speed)  ;

            }
            else
            {
                updateEnabled = false;
                GameManager.Instance.TotalCoin++;
                this.gameObject.SetActive(false);
            }
            
        }

    }
    public void Reset()
    {
        enabled = true;
        updateEnabled = true;
    }
}
