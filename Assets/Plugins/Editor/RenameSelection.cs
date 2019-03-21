using UnityEngine;
using UnityEditor;

public class RenameSelection : ScriptableWizard
{
    public GameObject[] ObjectsToRename;
    public string newName;
    public bool numberSuffix;

    [MenuItem("Custom/Rename GameObjects")]
    static void CreateWizard()
    {
        var wizard = DisplayWizard<RenameSelection>("Rename GameObjects", "Rename", "Cancel");
        wizard.ObjectsToRename = Selection.gameObjects;

        var prefillName = Selection.gameObjects[0].transform.name;
        int spaceIndex = prefillName.IndexOf(' ');
        wizard.newName = spaceIndex > 0
            ? prefillName.Substring(0, spaceIndex)
            : prefillName;
    }

    void OnWizardCreate()
    {
        for (var index = 0; index < ObjectsToRename.Length; index++)
        {
            GameObject go = ObjectsToRename[index];
            go.transform.name = numberSuffix ? string.Format("{0}{1}", newName, index) : newName;
        }
    }
}
