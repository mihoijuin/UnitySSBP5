using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainChara : MonoBehaviour
{
    private Material charaMat;

    private Vector2 initPos = new Vector2(0, 10);
    private float landDuration = 0.1f;
    private Ease landEase = Ease.InExpo;
    private float landScaleDuration = 0.1f;
    private Ease landScaleEase = Ease.OutBack;

    private float rotateDuration = 0.35f;
    private Ease rotateEase = Ease.InBack;

    private float prepareDuration = 0.5f;
    private Ease prepareEase = Ease.OutQuart;

    private void Awake()
    {
        this.charaMat = GetComponent<SpriteRenderer>().material;
    }

    public void Landing(float landPosY)
    {
        this.transform.position = this.initPos;
        this.transform.DOMoveY(landPosY, this.landDuration).SetEase(this.landEase)
            .OnComplete(()=> {
                AppUtil.SetMaterialFloat(this.charaMat, "_ScaleX", 0.2f, 1, this.landScaleDuration, this.landScaleEase);
                AppUtil.SetMaterialFloat(this.charaMat, "_ScaleY", 1.7f, 1, this.landScaleDuration, this.landScaleEase);
            });
    }

    public IEnumerator SlideOut()
    {
        AppUtil.SetMaterialFloat(this.charaMat, "_RotateZ", 0, -50, this.rotateDuration, this.rotateEase);
        AppUtil.SetMaterialFloat(this.charaMat, "_ScaleX", 1, 0.8f, this.rotateDuration, this.rotateEase);
        AppUtil.SetMaterialFloat(this.charaMat, "_ScaleY", 1, 0.95f, this.rotateDuration, this.rotateEase);
        yield return AppUtil.Wait(this.rotateDuration+0.01f);
        yield return this.transform.DOMoveX(this.transform.position.x-2, prepareDuration).SetEase(prepareEase).WaitForCompletion();
        this.transform.DOMoveX(13, 0.2f).SetEase(Ease.OutExpo);
    }
}
