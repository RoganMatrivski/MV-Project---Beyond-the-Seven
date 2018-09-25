using System;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(fadeBlackRenderer), PostProcessEvent.AfterStack, "Custom/Fade Black")]
public sealed class fadeBlack : PostProcessEffectSettings
{
    [Range(0f, 1f)]
    public FloatParameter blend = new FloatParameter { value = 0.5f };
}

public sealed class fadeBlackRenderer : PostProcessEffectRenderer<fadeBlack>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Shader/fadeBlack"));

        sheet.properties.SetFloat("_Blend", settings.blend);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);

        //throw new NotImplementedException();
    }
}