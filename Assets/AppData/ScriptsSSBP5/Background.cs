using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Image landOff = null;
    private Material landOffMat;
    private float landEffDuration = 0.3f;
    private Ease landEffEase = Ease.InQuart;

    private void Awake() {
        landOffMat = landOff.material;
    }

    public IEnumerator LandEff(Vector2 landPos)
    {
        landPos.y -= 2f;
        float landY = Camera.main.WorldToViewportPoint(landPos).y;
        landOffMat.SetFloat("_STY", landY);

        float startValue = 0;
        float endX = 0.43f;
        float endY = 0.13f;
        float endWidth = 0.03f;

        AppUtil.SetMaterialFloat(landOffMat, "_EllipseX", startValue, endX, landEffDuration, landEffEase);
        AppUtil.SetMaterialFloat(landOffMat, "_EllipseY", startValue, endY, landEffDuration, landEffEase);
        AppUtil.SetMaterialFloat(landOffMat, "_EllipseWidth", startValue, endWidth, landEffDuration, landEffEase);

        yield return AppUtil.Wait(landEffDuration+0.05f);
        AppUtil.SetMaterialFloat(landOffMat, "_EllipseWidth", endWidth, 0, 0.2f, Ease.OutQuart);

    }

    public IEnumerator RemainLine(float slideDuration)
    {
        AppUtil.SetMaterialFloat(landOffMat, "_RemainWidth", 0, 1f, slideDuration, Ease.OutExpo);
        yield return AppUtil.Wait(slideDuration+0.5f);
        AppUtil.SetMaterialFloat(landOffMat, "_RemainWidth", 1, 1.5f, 1.5f, Ease.OutQuart);
    }

    public void BlackOut()
    {
        AppUtil.SetMaterialFloat(landOffMat, "_RemainWidth", 1.5f, 100, 1f, Ease.InQuart);
    }

    public void WhiteOut()
    {
        AppUtil.SetMaterialFloat(landOffMat, "_RemainWidth", 100, 0, 2f, Ease.OutQuart);
    }
}
