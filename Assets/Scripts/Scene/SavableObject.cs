using UnityEngine;

public class SavableObject : MonoBehaviour
{
    [Header("��������� ����������")]
    public string saveKey = "object_"; // ������� ����

    [Header("��������� ���������")]
    public bool savePosition = true;
    public bool saveRotation = true;
    public bool saveScale = true;
    public bool saveActiveState = true;

    [Header("�������������� ����������")]
    public MonoBehaviour[] extraComponents; // ������� ��� ���. ����������

    private string GetFullKey(string suffix) => $"{saveKey}_{name}_{suffix}";

    public void SaveObject()
    {
        // 1. ��������� ��������� (�� ������)
        if (savePosition)
            ES3.Save(GetFullKey("position"), transform.position);

        if (saveRotation)
            ES3.Save(GetFullKey("rotation"), transform.rotation);

        if (saveScale)
            ES3.Save(GetFullKey("scale"), transform.localScale);

        // 2. ��������� ����������
        if (saveActiveState)
            ES3.Save(GetFullKey("active"), gameObject.activeSelf);

        // 3. ��������� ��� ����������
        foreach (var component in GetComponents<MonoBehaviour>())
        {
            if (component != null && component != this)
                ES3.Save(GetFullKey(component.GetType().Name), component);
        }

        // 4. �������������� ����������
        if (extraComponents != null)
        {
            foreach (var component in extraComponents)
            {
                if (component != null)
                    ES3.Save(GetFullKey("extra_" + component.GetType().Name), component);
            }
        }
    }

    public void LoadObject()
    {
        // 1. ��������� ���������
        if (savePosition && ES3.KeyExists(GetFullKey("position")))
            transform.position = ES3.Load<Vector3>(GetFullKey("position"));

        if (saveRotation && ES3.KeyExists(GetFullKey("rotation")))
            transform.rotation = ES3.Load<Quaternion>(GetFullKey("rotation"));

        if (saveScale && ES3.KeyExists(GetFullKey("scale")))
            transform.localScale = ES3.Load<Vector3>(GetFullKey("scale"));

        // 2. ��������� ����������
        if (saveActiveState && ES3.KeyExists(GetFullKey("active")))
            gameObject.SetActive(ES3.Load<bool>(GetFullKey("active")));

        // 3. ��������� ����������
        foreach (var component in GetComponents<MonoBehaviour>())
        {
            if (component != null && component != this && ES3.KeyExists(GetFullKey(component.GetType().Name)))
                ES3.LoadInto(GetFullKey(component.GetType().Name), component);
        }

        // 4. �������������� ����������
        if (extraComponents != null)
        {
            foreach (var component in extraComponents)
            {
                string key = GetFullKey("extra_" + component.GetType().Name);
                if (component != null && ES3.KeyExists(key))
                    ES3.LoadInto(key, component);
            }
        }
    }
}