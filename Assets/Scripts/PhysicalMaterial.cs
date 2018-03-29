using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMaterial : MonoBehaviour {

    public enum physMaterial { fallback, stone, ice, flesh, metal, wood };
    public physMaterial material = physMaterial.fallback;

}
