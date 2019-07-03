using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainChara : MonoBehaviour
{
    [SerializeField]
    private Material bgMat = null;
    
    private Vector2 initPos = new Vector2(0, 9);
    private Vector2 landPos = new Vector2(0, -1.2f);
    private float landDuration = 0.2f;
    private float landEffDuration = 0.4f;
    private Ease landEffEase = Ease.InQuart;

    public void Landing()
    {
        this.transform.position = initPos;
        this.transform.DOMoveY(landPos.y, landDuration).SetEase(Ease.InExpo);
        StartCoroutine(FireLandEff());
    }

    private void SetMaterialFloat(string property, float startValue, float endValue, float duration, Ease ease)
    {
        DOTween.To(
            ()=> startValue,
            x =>
            {
                startValue = x;
                bgMat.SetFloat(property, startValue);
            },
            endValue,
            duration
        ).SetEase(ease);
    }

    private IEnumerator FireLandEff()
    {
        yield return AppUtil.Wait(landDuration-0.2f);

        landPos.y -= 2f;
        float landY = Camera.main.WorldToViewportPoint(landPos).y;
        bgMat.SetFloat("_STY", landY);

        float startValue = 0;
        float endX = 0.43f;
        float endY = 0.13f;
        float endWidth = 0.03f;
        // x幅
        SetMaterialFloat("_EllipseX", startValue, endX, landEffDuration, landEffEase);
        SetMaterialFloat("_EllipseY", startValue, endY, landEffDuration, landEffEase);
        SetMaterialFloat("_Width", startValue, endWidth, landEffDuration, landEffEase);

        yield return AppUtil.Wait(landEffDuration+0.05f);
        SetMaterialFloat("_Width", endWidth, 0, 0.2f, Ease.OutQuart);

    }
}
