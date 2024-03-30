using UnityEngine;

[System.Serializable]
public class SerializableInterface<T> where T : class
{
    [SerializeField]
    private Object obj;
    private T m_Instance = null;
    public T Instance { get => GetInstance(); set => SetInstance(value); }
    public T GetInstance()
    {
        if (m_Instance == null || (object)m_Instance != obj)
        {
            if (obj == null)
                SetInstance(null);
            else if (obj is T inst)
                m_Instance = inst;
            else if (obj is GameObject go && go.TryGetComponent<T>(out inst))
                m_Instance = inst;
            else
                SetInstance(null);
        }
        return m_Instance;
    }
    void SetInstance(T aInstance)
    {
        m_Instance = aInstance;
        obj = m_Instance as Object;
    }
}

#if UNITY_EDITOR
namespace B83.EditorPropertyDrawers
{
    using System.Collections.Generic;
    using UnityEditor;
    [CustomPropertyDrawer(typeof(SerializableInterface<>), true)]
    public class SerializableInterfacePropertyDrawer : PropertyDrawer
    {
        private System.Type m_GenericType = null;
        private List<Component> m_List = new List<Component>();
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_GenericType == null)
            {
                System.Type fieldType = fieldInfo.FieldType;
                if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    // when used in a List<>, grab the actual type from the generic argument of the List
                    fieldType = fieldInfo.FieldType.GetGenericArguments()[0];
                }
                else if (fieldType.IsArray)
                {
                    // when used in an array, grab the actual type from the element type.
                    fieldType = fieldType.GetElementType();
                }

                var types = fieldType.GetGenericArguments();
                if (types != null && types.Length == 1)
                    m_GenericType = types[0];
            }
            var obj = property.FindPropertyRelative("obj");
            EditorGUI.BeginChangeCheck();
            var newObj = EditorGUI.ObjectField(position, label, obj.objectReferenceValue, typeof(UnityEngine.Object), true);
            if (EditorGUI.EndChangeCheck())
            {
                if (newObj == null)
                    obj.objectReferenceValue = null;
                else if (m_GenericType.IsAssignableFrom(newObj.GetType()))
                    obj.objectReferenceValue = newObj;
                else if (newObj is GameObject go)
                {
                    m_List.Clear();
                    go.GetComponents(m_GenericType, m_List);
                    if (m_List.Count == 1)
                        obj.objectReferenceValue = m_List[0];
                    else
                    {
                        GenericMenu m = new GenericMenu();
                        int n = 1;
                        foreach (var item in m_List)
                            m.AddItem(new GUIContent((n++).ToString() + " " + item.GetType().Name), false, a => {
                                obj.objectReferenceValue = (Object)a;
                                obj.serializedObject.ApplyModifiedProperties();
                            }, item);
                        m.ShowAsContext();
                    }
                }
                else
                    Debug.LogWarning("Dragged object is not compatible with " + m_GenericType.Name);
            }
        }
    }
}
#endif