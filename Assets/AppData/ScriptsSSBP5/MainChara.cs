using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainChara : MonoBehaviour
{
    [SerializeField]
    private Material bgMat = null;
    private Material charaMat;

    private Vector2 initPos = new Vector2(0, 10);
    private Vector2 landPos = new Vector2(0, -1.2f);

    private float landDuration = 0.1f;
    private Ease landEase = Ease.InExpo;

    private float landScaleDuration = 0.1f;
    private Ease landScaleEase = Ease.OutBack;

    private float landEffDuration = 0.3f;
    private Ease landEffEase = Ease.InQuart;

    private float rotateDuration = 0.5f;
    private Ease rotateEase = Ease.InBack;


    private Title title;

    private void Awake()
    {
        this.title = FindObjectOfType<Title>();
        this.charaMat = GetComponent<SpriteRenderer>().material;
    }

    public IEnumerator Landing()
    {
        this.transform.position = initPos;
        this.transform.DOMoveY(landPos.y, landDuration).SetEase(landEase)
            .OnComplete(()=> {
                AppUtil.SetMaterialFloat(charaMat, "_ScaleX", 0.2f, 1, landScaleDuration, landScaleEase);
                AppUtil.SetMaterialFloat(charaMat, "_ScaleY", 1.7f, 1, landScaleDuration, landScaleEase);
            });
        yield return LandEff();
        yield return AppUtil.Wait(0.3f);
        yield return SlideOut();
    }

    private IEnumerator LandEff()
    {
        landPos.y -= 2f;
        float landY = Camera.main.WorldToViewportPoint(landPos).y;
        bgMat.SetFloat("_STY", landY);

        float startValue = 0;
        float endX = 0.43f;
        float endY = 0.13f;
        float endWidth = 0.03f;

        AppUtil.SetMaterialFloat(bgMat, "_EllipseX", startValue, endX, landEffDuration, landEffEase);
        AppUtil.SetMaterialFloat(bgMat, "_EllipseY", startValue, endY, landEffDuration, landEffEase);
        AppUtil.SetMaterialFloat(bgMat, "_EllipseWidth", startValue, endWidth, landEffDuration, landEffEase);

        yield return AppUtil.Wait(landEffDuration+0.05f);
        AppUtil.SetMaterialFloat(bgMat, "_EllipseWidth", endWidth, 0, 0.2f, Ease.OutQuart);

    }

    private IEnumerator SlideOut()
    {
        AppUtil.SetMaterialFloat(charaMat, "_RotateZ", 0, -50, rotateDuration, rotateEase);
        AppUtil.SetMaterialFloat(charaMat, "_ScaleX", 1, 0.8f, rotateDuration, rotateEase);
        AppUtil.SetMaterialFloat(charaMat, "_ScaleY", 1, 0.95f, rotateDuration, rotateEase);
        yield return AppUtil.Wait(0.8f);
        yield return this.transform.DOMoveX(this.transform.position.x-2, 0.5f).WaitForCompletion();
        this.transform.DOMoveX(13, 0.2f).SetEase(Ease.OutExpo);

        yield return AppUtil.Wait(0.15f);
        yield return title.ShowTitle();
    }
}
