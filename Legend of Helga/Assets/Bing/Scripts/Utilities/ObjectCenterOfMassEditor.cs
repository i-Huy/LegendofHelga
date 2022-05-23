#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectCenterOfMass))]
public class ObjectCenterOfMassEditor : Editor
{

    protected bool editEnabled = false;

    protected virtual void OnSceneGUI()
    {

        // Reference to component we are editing
        ObjectCenterOfMass component = (ObjectCenterOfMass)target;

        // Map local coords to world
        Vector4 comv4 = new Vector4(
            component.centerOfMass.x,
            component.centerOfMass.y,
            component.centerOfMass.z,
            1f);
        Vector4 wcomv4 = component.transform.localToWorldMatrix * comv4;
        Vector3 handlePos = new Vector3(wcomv4.x, wcomv4.y, wcomv4.z);

        if (editEnabled)
        {

            EditorGUI.BeginChangeCheck();

            // Draw handles
            Vector3 newHandlePos = Handles.PositionHandle(
                handlePos, component.transform.rotation);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(
                    component, "Change Center of Mass Position");

                // Go back to local coords
                Vector4 nhpv4 = new Vector4(
                    newHandlePos.x, newHandlePos.y, newHandlePos.z, 1f);
                Vector4 new_com = component.transform.worldToLocalMatrix *
                    nhpv4;

                component.centerOfMass = new Vector3(new_com.x, new_com.y, new_com.z);

                if (EditorApplication.isPlaying)
                {
                    component.AssignCenterOfMass();
                }
            }
        }
    }


    void OnSelectionChange()
    {
        this.editEnabled = false;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(
            "Edit the center of mass or specify" +
            "a GameObject to serve as COM", MessageType.Info);

        ObjectCenterOfMass myScript = (ObjectCenterOfMass)target;

        myScript.comMarker = (GameObject)EditorGUILayout.ObjectField(
            new GUIContent(
                "CoM Marker Object", "An optional marker GameObject used to identify where the center of mass is. If assigned non-null, it will be used to determine the centerOfMass"),
            myScript.comMarker, typeof(GameObject), true);

        if (myScript.comMarker == null)
        {
            string btnTxt = !this.editEnabled ?
                "Edit Center of Mass Position" : "Stop Editing";

            if (GUILayout.Button(btnTxt))
            {

                this.editEnabled = !this.editEnabled;
                EditorUtility.SetDirty(target);
            }

            myScript.centerOfMass = EditorGUILayout.Vector3Field(
                new GUIContent(
                    "Center of Mass", "The relative position defining the center of mass that will be used in physics simulation. Will be overridden by centerOfMassMarker if non-null."),
                myScript.centerOfMass);
        }

        myScript.continuousUpdate = EditorGUILayout.Toggle(
            new GUIContent(
                "Continuous Updating", "Whether the centerOfMass is re-evaluated every FixedUpdate()"),
                myScript.continuousUpdate);
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    //[DrawGizmo(GizmoType.Pickable | GizmoType.InSelectionHierarchy)]
    static void DrawGizmoRigidbodyCenterOfMass(
        ObjectCenterOfMass obj, GizmoType gizmoType)
    {

        // Draw a sphere at the transform's position
        Gizmos.color = Color.cyan;
        Gizmos.matrix = obj.transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(obj.centerOfMass, 0.1f);
    }

}


#endif
