using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Title : MonoBehaviour
{
    private Text[] textArray;
    private RectTransform[] textRect;
    private float showY = -1;
    private float hideY = -4;
    private float moveDuration = 0.3f;

    private void Awake() {
        this.textArray = GetComponentsInChildren<Text>();
        this.textRect = new RectTransform[this.textArray.Length];
        for(int i=0; i<this.textRect.Length; i++)
        {
            this.textRect[i] = this.textArray[i].GetComponent<RectTransform>();
        }
    }

    public IEnumerator ShowTitle()
    {
        foreach(RectTransform rect in this.textRect.Take(this.textRect.Length-1))
        {
            yield return AppUtil.Wait(0.03f);
            rect.DOMoveY(this.showY, this.moveDuration).SetEase(Ease.OutBack);
        }

        yield return AppUtil.Wait(0.5f);
        this.textRect[this.textRect.Length-1].DOMoveY(this.showY, this.moveDuration+0.1f).SetEase(Ease.OutBack);
    }

    public IEnumerator HideTitle()
    {
        foreach(RectTransform rect in this.textRect.Take(this.textRect.Length-1))
        {
            yield return AppUtil.Wait(0.08f);
            rect.DOMoveY(this.hideY, this.moveDuration).SetEase(Ease.InBack);
        }

        yield return AppUtil.Wait(2);

        RectTransform lastText = this.textRect[this.textRect.Length-1];
        yield return lastText.DOScaleX(-1, 0.5f).WaitForCompletion();
        yield return AppUtil.Wait(1);
        yield return lastText.DOScaleX(1, 0.5f).WaitForCompletion();
        yield return AppUtil.Wait(1);
        yield return lastText.DOPunchPosition(new Vector2(0, 100), 0.2f).WaitForCompletion();
        yield return lastText.DOPunchPosition(new Vector2(0, 100), 0.2f).WaitForCompletion();
        yield return AppUtil.Wait(0.5f);
        yield return lastText.DOMoveY(lastText.transform.position.y+2f, 0.3f).WaitForCompletion();
        yield return lastText.DOScaleY(-1, 0.15f).WaitForCompletion();
        yield return AppUtil.Wait(0.1f);
        lastText.DOMoveY(this.hideY, this.moveDuration).SetEase(Ease.InBack);
    }

    public void Whiten()
    {
        foreach(Text text in textArray)
        {
            text.DOColor(Color.white, 0.1f);
        }
    }

    public void Blacken()
    {
        foreach(Text text in textArray)
        {
            text.DOColor(Color.black, 0.5f);
        }
    }
}
