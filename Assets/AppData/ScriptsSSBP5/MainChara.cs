using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainChara : MonoBehaviour
{
    [SerializeField]
    private Material bgMat = null;
    private Material charaMat;

    private Vector2 initPos = new Vector2(0, 9);
    private Vector2 landPos = new Vector2(0, -1.2f);
    private float landDuration = 0.2f;
    private float landEffDuration = 0.35f;
    private Ease landEffEase = Ease.InQuart;

    private Title title;

    private void Awake()
    {
        this.title = FindObjectOfType<Title>();
        this.charaMat = GetComponent<SpriteRenderer>().material;
        StartCoroutine(SlideOut());
    }

    private IEnumerator SlideOut()
    {
        yield return AppUtil.Wait(1f);
        yield return this.transform.DOLocalRotate(new Vector3(0,0,-40), 0.35f).SetEase(Ease.InOutBack).WaitForCompletion();
        yield return AppUtil.Wait(0.1f);
        this.transform.DOMoveX(13, 0.3f).SetEase(Ease.InBack);

        yield return AppUtil.Wait(0.15f);
        yield return title.ShowTitle();
    }

    public void Landing()
    {
        this.transform.position = initPos;
        this.transform.DOMoveY(landPos.y, landDuration).SetEase(Ease.InExpo)
            .OnComplete(()=> {
                SetMaterialFloat(charaMat, "_ScaleX", 0.5f, 1, landEffDuration*0.3f, Ease.OutBack);
                SetMaterialFloat(charaMat, "_ScaleY", 1.4f, 1, landEffDuration*0.3f, Ease.OutBack);
            });
        StartCoroutine(LandEff());
    }

    private void SetMaterialFloat(Material mat, string property, float startValue, float endValue, float duration, Ease ease)
    {
        DOTween.To(
            ()=> startValue,
            x =>
            {
                startValue = x;
                mat.SetFloat(property, startValue);
            },
            endValue,
            duration
        ).SetEase(ease);
    }

    private IEnumerator LandEff()
    {
        yield return AppUtil.Wait(landDuration-0.2f);

        landPos.y -= 2f;
        float landY = Camera.main.WorldToViewportPoint(landPos).y;
        bgMat.SetFloat("_STY", landY);

        float startValue = 0;
        float endX = 0.43f;
        float endY = 0.13f;
        float endWidth = 0.03f;

        SetMaterialFloat(bgMat, "_EllipseX", startValue, endX, landEffDuration, landEffEase);
        SetMaterialFloat(bgMat, "_EllipseY", startValue, endY, landEffDuration, landEffEase);
        SetMaterialFloat(bgMat, "_Width", startValue, endWidth, landEffDuration, landEffEase);

        yield return AppUtil.Wait(landEffDuration+0.05f);
        SetMaterialFloat(bgMat, "_Width", endWidth, 0, 0.2f, Ease.OutQuart);

    }
}
