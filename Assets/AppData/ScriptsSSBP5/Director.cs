﻿using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour
{
    private Vector2 charaLandPos = new Vector2(0, -1.2f);

    [SerializeField]
    private OpPostEffect op = null;
    [SerializeField]
    private MainChara chara = null;
    [SerializeField]
    private Background bg = null;
    [SerializeField]
    private Title title = null;

    private void Awake() {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return AppUtil.Wait(1f);
        yield return this.op.PlayOP();

        this.chara.Landing(this.charaLandPos.y);
        yield return this.bg.LandEff(this.charaLandPos);

        yield return AppUtil.Wait(0.3f);
        yield return this.chara.SlideOut();

        yield return AppUtil.Wait(0.15f);
        yield return title.ShowTitle();
    }
}
