﻿using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEditor.ShaderGraph;

namespace UnityEditor.Rendering.HighDefinition.ShaderGraph
{
    class HDDecalTarget : ITargetImplementation
    {
        public Type targetType => typeof(DecalTarget);
        public string displayName => "HDRP";
        public string passTemplatePath => $"{HDUtils.GetHDRenderPipelinePath()}Editor/Material/Decal/ShaderGraph/DecalPass.template";
        public string sharedTemplateDirectory => $"{HDUtils.GetHDRenderPipelinePath()}Editor/ShaderGraph/Templates";

        public bool IsValid(IMasterNode masterNode)
        {
            return GetSubShaderDescriptorFromMasterNode(masterNode) != null;
        }

        public bool IsPipelineCompatible(RenderPipelineAsset currentPipeline)
        {
            return currentPipeline is HDRenderPipelineAsset;
        }

        public void SetupTarget(ref TargetSetupContext context)
        {
            context.AddAssetDependencyPath(AssetDatabase.GUIDToAssetPath("61d739b0177943f4d858e09ae4b69ea2")); // DecalTarget
            context.AddAssetDependencyPath(AssetDatabase.GUIDToAssetPath("21bb2072667892445b27f3e9aad497af")); // HDRPDecalTarget

            var subShader = GetSubShaderDescriptorFromMasterNode(context.masterNode);
            if (subShader != null)
                context.SetupSubShader(subShader.Value);
        }

        public SubShaderDescriptor? GetSubShaderDescriptorFromMasterNode(IMasterNode masterNode)
        {
            switch (masterNode)
            {
                case DecalMasterNode _:
                    return HDSubShaders.Decal;
                default:
                    return null;
            }
        }
    }
}
