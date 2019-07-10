using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Title : MonoBehaviour
{
    private Text[] textArray;

    private void Awake() {
        this.textArray = GetComponentsInChildren<Text>();
    }

    public IEnumerator ShowTitle()
    {
        foreach(Text text in textArray.Take(textArray.Length-1))
        {
            yield return AppUtil.Wait(0.05f);
            text.GetComponent<RectTransform>().DOMoveY(-1f, 0.3f).SetEase(Ease.OutBack);
        }

        yield return AppUtil.Wait(0.5f);
        textArray[textArray.Length-1].GetComponent<RectTransform>().DOMoveY(-1f, 0.4f).SetEase(Ease.OutBack);
    }
}
