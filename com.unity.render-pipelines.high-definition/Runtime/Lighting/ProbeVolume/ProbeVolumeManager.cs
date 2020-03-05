using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
    public class ProbeVolumeManager
    {
        static private ProbeVolumeManager _instance = null;

        public static ProbeVolumeManager manager
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProbeVolumeManager();
                }
                return _instance;
            }
        }
        private ProbeVolumeManager()
        {
            Debug.Log("Constructing ProbeVolumeManager");
            volumes = new List<ProbeVolume>();
            OnEnable();
        }
        ~ProbeVolumeManager()
        {
            OnDisable();
        }

        public List<ProbeVolume> volumes = null;

        protected void OnEnable()
        {
            Debug.Log("ProbeVolumeManager.OnEnable");
            EnableBaking();
        }

        protected void OnDisable()
        {
            Debug.Log("ProbeVolumeManager.OnDisable");
            DisableBaking();
        }

        public void RegisterVolume(ProbeVolume volume)
        {
            if (volumes.Contains(volume))
                return;

            volumes.Add(volume);
            if (volume.GetID() != -1)
                volume.SetupPositions(true);
        }
        public void DeRegisterVolume(ProbeVolume volume)
        {
            if (!volumes.Contains(volume))
                return;

            volumes.Remove(volume);

            HDRenderPipeline hdrp = RenderPipelineManager.currentPipeline as HDRenderPipeline;
            if (hdrp != null)
                hdrp.ReleaseProbeVolumeFromAtlas(volume);

            // if (volume.GetID() != -1)
                // UnityEditor.Experimental.Lightmapping.SetAdditionalBakedProbes(volume.GetID(), null);
        }

        void EnableBaking()
        {
            Debug.Log("EnableBaking?");
            if (ShaderConfig.s_ProbeVolumes == 0)
                return;

            Debug.Log("EnableBaking");

            UnityEditor.Experimental.Lightmapping.additionalBakedProbesCompleted += OnProbesBakeCompleted;
            UnityEditor.Lightmapping.bakeCompleted += OnBakeCompleted;

            UnityEditor.Lightmapping.lightingDataCleared += OnLightingDataCleared;
            UnityEditor.Lightmapping.lightingDataAssetCleared += OnLightingDataAssetCleared;
        }

        void DisableBaking()
        {
            Debug.Log("DisableBaking?");
            if (ShaderConfig.s_ProbeVolumes == 0)
                return;

            Debug.Log("DisableBaking");

            UnityEditor.Experimental.Lightmapping.additionalBakedProbesCompleted -= OnProbesBakeCompleted;
            UnityEditor.Lightmapping.bakeCompleted -= OnBakeCompleted;

            UnityEditor.Lightmapping.lightingDataCleared -= OnLightingDataCleared;
            UnityEditor.Lightmapping.lightingDataAssetCleared -= OnLightingDataAssetCleared;
        }


        public void OnProbesBakeCompleted()
        {
            // System.Threading.Thread.Sleep(10000);
            Debug.Log("OnProbesBakeCompleted");
            foreach (var volume in volumes)
            {
                Debug.Log("OnProbesBakeCompleted for volume: " + volume.GetID());
                volume.OnProbesBakeCompleted();
            }
        }

        public void OnBakeCompleted()
        {
            foreach (var volume in volumes)
            {
                volume.OnBakeCompleted();
            }
        }

        public void OnLightingDataCleared()
        {
            foreach (var volume in volumes)
            {
                volume.OnLightingDataCleared();
            }
        }

        public void OnLightingDataAssetCleared()
        {
            foreach (var volume in volumes)
            {
                volume.OnLightingDataAssetCleared();
            }
        }

#if UNITY_EDITOR
        public void ReactivateProbes()
        {
            foreach (ProbeVolume v in volumes)
            {
                v.EnableBaking();
            }

            UnityEditor.Lightmapping.bakeCompleted -= ReactivateProbes;
        }
        public static void BakeSingle()
        {
            List<ProbeVolume> selectedProbeVolumes = new List<ProbeVolume>();

            foreach (GameObject go in UnityEditor.Selection.gameObjects)
            {
                ProbeVolume probeVolume = go.GetComponent<ProbeVolume>();
                if (probeVolume)
                    selectedProbeVolumes.Add(probeVolume);
            }

            foreach (ProbeVolume v in manager.volumes)
            {
                if (selectedProbeVolumes.Contains(v))
                    continue;

                v.DisableBaking();
            }

            UnityEditor.Lightmapping.bakeCompleted += manager.ReactivateProbes;
            UnityEditor.Lightmapping.BakeAsync();
        }
#endif
    }
}
