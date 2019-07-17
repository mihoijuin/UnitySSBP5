using System.Collections;
using UnityEngine;
using DG.Tweening;

public class OpPostEffect : MonoBehaviour
{
 	[SerializeField]
    private Material opMaterial = null;

    private string[] lineProperties = new string[] { "_VLWidth", "_HLWidth" };
    private float startValue = 0.008f;
    private float[] waitTimes = new float[] { 0.05f, 0.8f, 1.2f };
    private float[] endValues = new float[] { 0.001f, 0.07f, 0.8f };
    private float[] durations = new float[] { 0.0005f, 0.15f, 0.8f };
    private Ease[] eases = new Ease[] { Ease.InExpo, Ease.InExpo, Ease.InQuart };

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
 	{
 		Graphics.Blit (src, dest, opMaterial);
 	}

    private void ThickenOPLine(float startValue, float endValue, float duration, Ease ease)
    {
        foreach(string property in lineProperties)
        {
            AppUtil.SetMaterialFloat(opMaterial, property, startValue, endValue, duration, ease);
        }
    }

    public IEnumerator PlayOP() {
        // 縦横に白いラインが入る
        foreach(string property in lineProperties)
        {
            opMaterial.SetFloat(property, this.startValue);
            yield return AppUtil.Wait(this.waitTimes[0]);
            AppUtil.SetMaterialFloat(opMaterial, property, this.startValue, this.endValues[0], this.durations[0], this.eases[0]);
        }

        // 白地が増えていき真っ白な画面になる
        for(int i=1; i<this.waitTimes.Length; i++)
        {
            yield return AppUtil.Wait(this.waitTimes[i]);
            ThickenOPLine(this.endValues[i-1], this.endValues[i], this.durations[i], this.eases[i]);
        }

        yield return AppUtil.Wait(1.5f);
        this.enabled = false;
    }
}
