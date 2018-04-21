using UnityEditor;
using UnityEngine;

public class MeshCombiner: EditorWindow
{
    GameObject[] g;
    Mesh[] m;

    string instr = "Select the objects you would like to combine in the scene view. Then press combine to merge them together.";
    
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/MeshCombiner")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MeshCombiner));
    }
    
    void OnGUI()
    {
        GUILayout.Label ("Selection", EditorStyles.boldLabel);

        EditorGUILayout.SelectableLabel(instr, GUILayout.Width(position.width));

        if(GUILayout.Button("Combine", GUILayout.Width(position.width)))
        {
            Combine();
        }

    }

    private void Combine()
    {
        int amountSelected = Selection.gameObjects.Length;

        MeshFilter[] mf = new MeshFilter[amountSelected];
        CombineInstance[] ci = new CombineInstance[amountSelected];

        for(int i = 0; i < amountSelected; i++)
        {
            mf[i] = Selection.gameObjects[i].GetComponent<MeshFilter>();

            ci[i].mesh = mf[i].sharedMesh;
            ci[i].transform = mf[i].transform.localToWorldMatrix;

        }

        GameObject obj = new GameObject("CombinedMeshes", typeof(MeshFilter), typeof(MeshRenderer));
        obj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(ci);
        obj.GetComponent<MeshRenderer>().sharedMaterial = new Material(mf[0].gameObject.GetComponent<MeshRenderer>().sharedMaterial);


    }
}