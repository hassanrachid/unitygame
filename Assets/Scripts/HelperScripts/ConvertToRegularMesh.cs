using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToRegularMesh : MonoBehaviour {



    [ContextMenu("Convert to Regular Mesh")]
    void Convert() {
        SkinnedMeshRenderer skin = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer mesh = gameObject.AddComponent<MeshRenderer>();
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();

        filter.sharedMesh = skin.sharedMesh;
        mesh.sharedMaterials = skin.sharedMaterials;

        DestroyImmediate(skin);
        DestroyImmediate(this);
    }

}

