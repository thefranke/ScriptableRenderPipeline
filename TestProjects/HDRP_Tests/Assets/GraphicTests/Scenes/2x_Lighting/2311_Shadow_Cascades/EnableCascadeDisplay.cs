using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class EnableCascadeDisplay : MonoBehaviour
{
    HDRenderPipeline hdrp;

    public void ShowCascades()
    {
        hdrp = RenderPipelineManager.currentPipeline as HDRenderPipeline;

        if (hdrp != null)
        {
            hdrp.debugDisplaySettings.SetDebugLightingMode(DebugLightingMode.VisualizeCascade);
        }
    }
}
