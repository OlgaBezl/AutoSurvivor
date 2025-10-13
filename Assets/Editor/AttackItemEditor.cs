using Scripts.Items.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using AttackType = Scripts.Attack.AttackType;

[CustomEditor(typeof(AttackItem))]
public class AttackItemEditor : Editor
{
    private SerializedProperty _spriteProperty;
    private SerializedProperty _nameProperty;
    private SerializedProperty _typeProperty;
    private SerializedProperty _attackProperty;
    private SerializedProperty _speedProperty;
    private SerializedProperty _lifeTimeProperty;
    private SerializedProperty _radiusProperty;
    private SerializedProperty _radiusVariationProperty;
    private SerializedProperty _heightProperty;
    private SerializedProperty _heightVariationProperty;
    private SerializedProperty _distanceProperty;
    private SerializedProperty _spawnIntervalProperty;
    private SerializedProperty _canTurnProperty; 
    private SerializedProperty _superVersionProperty; 
    private SerializedProperty _tupleItemProperty;

    private void OnEnable()
    {
        _spriteProperty = serializedObject.FindProperty("Sprite");
        _nameProperty = serializedObject.FindProperty("Name");
        _typeProperty = serializedObject.FindProperty("Type");
        _attackProperty = serializedObject.FindProperty("Attack");
        _speedProperty = serializedObject.FindProperty("Speed");
        _lifeTimeProperty = serializedObject.FindProperty("LifeTime");
        _radiusProperty = serializedObject.FindProperty("Radius");
        _radiusVariationProperty = serializedObject.FindProperty("RadiusVariation");
        _heightProperty = serializedObject.FindProperty("Height");
        _heightVariationProperty = serializedObject.FindProperty("HeightVariation");
        _distanceProperty = serializedObject.FindProperty("Distance");
        _spawnIntervalProperty = serializedObject.FindProperty("SpawnInterval");
        _canTurnProperty = serializedObject.FindProperty("CanTurn");
        _superVersionProperty = serializedObject.FindProperty("SuperVersion");
        _tupleItemProperty = serializedObject.FindProperty("TupleItem");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_spriteProperty, new GUIContent("Sprite"));
        EditorGUILayout.PropertyField(_nameProperty, new GUIContent("Name"));
        EditorGUILayout.PropertyField(_typeProperty, new GUIContent("Type"));
        EditorGUILayout.PropertyField(_attackProperty, new GUIContent("Attack value"));
        EditorGUILayout.PropertyField(_superVersionProperty, new GUIContent("Super version"));
        EditorGUILayout.PropertyField(_tupleItemProperty, new GUIContent("Tuple passive item"));

        AttackType currentType = (AttackType)_typeProperty.intValue;

        if (currentType == AttackType.Static)
        {
            EditorGUILayout.PropertyField(_canTurnProperty, new GUIContent("Can turn"));
        }
        else
        {
            EditorGUILayout.PropertyField(_speedProperty, new GUIContent("Speed"));
            EditorGUILayout.PropertyField(_lifeTimeProperty, new GUIContent("Life time"));
            EditorGUILayout.PropertyField(_spawnIntervalProperty, new GUIContent("Spawn interval"));
        }

        if (currentType == AttackType.BunchProjectile)
        {
            EditorGUILayout.PropertyField(_radiusProperty, new GUIContent("Radius"));
            EditorGUILayout.PropertyField(_radiusVariationProperty, new GUIContent("Radius variation"));
            EditorGUILayout.PropertyField(_heightProperty, new GUIContent("Height"));
            EditorGUILayout.PropertyField(_heightVariationProperty, new GUIContent("Height variation"));
            EditorGUILayout.PropertyField(_distanceProperty, new GUIContent("Distance"));
        }

        if (currentType == AttackType.CircleProjectile)
        {
            EditorGUILayout.PropertyField(_radiusProperty, new GUIContent("Radius"));
        }

        EditorGUILayout.Space();
        DrawAttackTypeInfo();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawAttackTypeInfo()
    {
        //EditorGUILayout.HelpBox("—нар€ды летают по кругу", MessageType.Info);
        EditorGUILayout.LabelField("ќписание типов атак:", EditorStyles.boldLabel);
        EditorGUILayout.LabelField($"Х {AttackType.CircleProjectile} - снар€ды летают по кругу");
        EditorGUILayout.LabelField($"Х {AttackType.BunchProjectile} - снар€ды бросаютс€ охапкой (по случайной пораболе)");
        EditorGUILayout.LabelField($"Х {AttackType.RandomProjectile} - снар€д летит по пр€мой в случайную сторону");
        EditorGUILayout.LabelField($"Х {AttackType.SeekerProjectile} - снар€д летит по пр€мой к ближайшему врагу");
        EditorGUILayout.LabelField($"Х {AttackType.Static} - статична€ зона атаки вокруг геро€");
    }
}