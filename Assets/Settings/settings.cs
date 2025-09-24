using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static dialoge;

public class settings : MonoBehaviour
{
    public TextMeshProUGUI dropdown;
    public Camera PlayerCamera;
    public List<Material> TesalatedMaterails = new List<Material>();
    public Volume volume;
    public VolumeProfile VisualFedalityLevel0;
    public VolumeProfile VisualFedalityLevel1;
    public VolumeProfile VisualFedalityLevel2;
    public VolumeProfile VisualFedalityLevel3;
    public VolumeProfile VisualFedalityLevel4;

    public int TargetFps;
    public int VisualFedality;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        load_settings();
    }
    public void UpdateValues(int val)
    {
        VisualFedality = val;
    }
    public void UpdateFrameRate(int val)
    {
        TargetFps = (val + 1) * 30;
        print(TargetFps);
    }
    public void load_settings()
    {
        Application.targetFrameRate = TargetFps;
        //Screen.SetResolution(1280, 720, true);
        if (VisualFedality == 0) //No Ao no reflections no motion blur no lighting effects for older computers
        {
            volume.profile = VisualFedalityLevel0;
            Ajustments(18, 0);
            
        }
        if (VisualFedality == 1) //Visual fedality level 1 no ray tracing only screenspace lighting effects
        {
            volume.profile = VisualFedalityLevel1;
            Ajustments(90, 4);
        }
        else if (VisualFedality == 2) //Visual fedality level 2 only ambeint occlusion is ray traced 
        {
            volume.profile = VisualFedalityLevel2;
            Ajustments(90, 6);
        }
        else if (VisualFedality == 3) //Visual fedality level 3 reflections and AO are fully ray traced
        {
            volume.profile = VisualFedalityLevel3;
            Ajustments(90, 8);
        }
        else if (VisualFedality == 4) //Visual fedality level 4 reflections and AO are fully ray traced at a higher quality 
        {
            volume.profile = VisualFedalityLevel4;
            Ajustments(120, 12);
        }
    }
    public void Ajustments(int CameraDistance,int tesalation)
    {
        PlayerCamera.farClipPlane = CameraDistance;
        foreach (Material mat in TesalatedMaterails) 
        {
            mat.SetFloat("_TessellationFactor", tesalation);

        }
    }
}
