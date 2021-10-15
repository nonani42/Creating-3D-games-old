using System.Collections;
using UnityEditor;
using UnityEngine;

public class CustomWindow : EditorWindow
{
    public Color myColor;         // �������� �����
    public MeshRenderer GO;      // ������ �� ������ �������
    public Material myMaterial;
    private Transform mainCam;
    
    [MenuItem("Custom / Windows/ CustomWindow")]
    public static void CreateCustomWindow()
    {
        GetWindow(typeof(CustomWindow), false);
    }
    void OnGUI()
    {
        GO = EditorGUILayout.ObjectField("MeshRenderer", GO, typeof(MeshRenderer), true) as MeshRenderer;
        myMaterial = EditorGUILayout.ObjectField("Material", myMaterial, typeof(Material), true) as Material;
        if (GO && myMaterial)
        {
            myColor = RGBSlider(new Rect(10, 50, 200, 20), myColor);  // ��������� ����������������� ������ ��������� ��� ��������� ��������� �����
            GO.sharedMaterial.color = myColor; // �������� �������
        }
        else
        {
            if (GUI.Button(new Rect(10, 160, 200, 20), "CreateGO"))
            {
                mainCam = Camera.main.transform;
                GameObject tempGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                MeshRenderer tempMesh = tempGO.GetComponent<MeshRenderer>();
                tempMesh.sharedMaterial = myMaterial;
                tempGO.transform.position = new Vector3(mainCam.position.x, mainCam.position.y, mainCam.position.z + 15f);
                GO = tempMesh;
            }
        }
        if(GUI.Button(new Rect(10, 200, 200, 20), "DestroyGO"))
        {
            DestroyImmediate(GO.gameObject);
            GO = null;
        }
    }

    // ��������� ����������������� ��������
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMinValue, float sliderMaxValue, string labelText) // �� �������� MinValue
    {
        // ������ ������������� � ������������ � ������������ � ������� ������� � ������� 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // ������ Label �� ������

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // ����� ������� ��������
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, sliderMinValue, sliderMaxValue); // ������������ ������� � ��������� ��� ��������
        return sliderValue; // ���������� �������� ��������
    }

    // ��������� ������� ������� ������, ������ ������� �������� �� ���� ����
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // ��������� ���������������� �������, ������ ���
        rgb.r = LabelSlider(screenRect, rgb.r, 0.0f, 1.0f, "Red");

        // ������ ����������
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 0.0f, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 0.0f, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 0.0f, 1.0f, "alpha");

        return rgb; // ���������� ����
    }
}
