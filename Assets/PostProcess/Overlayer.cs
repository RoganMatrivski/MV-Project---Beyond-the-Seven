using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using UnityEngine.Video;

[ExecuteInEditMode]
public class Overlayer : MonoBehaviour {
    public bool show;

    public float mix;

    public Material mat;

    private Tween temp;

    public void showGlitch()
    {
        if (temp != null && !temp.IsComplete())
            temp.Kill();
        mix = 1;
    }

    public void stopGlitch()
    {
        temp = DOTween.To(() => mix, x => mix = x, 0, 0.5f);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
   //     if (show)
   //         mix = 1;
   //     else
			//mix = 0;

        mat.SetFloat("_Mix", mix); mat.SetFloat("_Displace", mix);
        Graphics.Blit(source, destination, mat);
    }
}
