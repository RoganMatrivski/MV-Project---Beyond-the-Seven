﻿using System;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(fadeWhiteRenderer), PostProcessEvent.AfterStack, "Custom/Fade White")]
public sealed class fadeWhite : PostProcessEffectSettings
{
    [Range(0f, 1f)]
    public FloatParameter blend = new FloatParameter { value = 0.5f };
}

public sealed class fadeWhiteRenderer : PostProcessEffectRenderer<fadeWhite>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Shader/fadeWhite"));

        sheet.properties.SetFloat("_Blend", settings.blend);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);

        //throw new NotImplementedException();
    }
}