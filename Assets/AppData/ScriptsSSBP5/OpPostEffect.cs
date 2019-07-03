using System.Collections;
using UnityEngine;
using DG.Tweening;

public class OpPostEffect : MonoBehaviour
{
 	[SerializeField]
    private Material opMaterial = null;

    private void Awake() {
        StartCoroutine(OP());
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
 	{
 		Graphics.Blit (src, dest, opMaterial);
 	}

    private IEnumerator SetMaterialFloat(float startValue, float endValue, float duration, Ease ease, float delay=0, bool initMode=false)
    {
        string[] properties = new string[] { "_VLWidth", "_HLWidth" };

        foreach(string property in properties)
        {
            if(initMode) opMaterial.SetFloat(property, startValue);
            yield return AppUtil.Wait(delay);
            DOTween.To(
                ()=> startValue,
                x =>
                {
                    startValue = x;
                    opMaterial.SetFloat(property, startValue);
                },
                endValue,
                duration
            ).SetEase(ease);
        }
    }

    private IEnumerator OP() {
        yield return AppUtil.Wait(1f);
        yield return SetMaterialFloat(0.008f, 0.001f, 0.0005f, Ease.InExpo, 0.1f, true);

        yield return AppUtil.Wait(0.8f);
        yield return SetMaterialFloat(0.001f, 0.07f, 0.15f, Ease.InExpo);

        yield return AppUtil.Wait(1.15f);
        yield return SetMaterialFloat(0.07f, 0.8f, 0.8f, Ease.InQuart);
    }
}
