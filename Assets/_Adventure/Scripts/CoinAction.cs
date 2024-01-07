using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAction : MonoBehaviour
{
    private Tween punchTween;
    // Start is called before the first frame update
    public void StopPunch()
    {
        if (punchTween != null && punchTween.IsActive())
        {
            punchTween.Kill();
        }
    }
    private void OnEnable()
    {
       // punchTween = transform.DOPunchPosition(new Vector3(0, 0.3f, 0), 10, 1);
    }
    private void OnDisable()
    {
       
    }
}
