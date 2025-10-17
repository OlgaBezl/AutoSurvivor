using Scripts.Items.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using AttackType = Scripts.Attack.AttackType;

[CustomEditor(typeof(AttackItemData))]
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
    private SerializedProperty _addProjectileWhenLevelingUpProperty;
    private SerializedProperty _increaseWhenLevelingUpProperty;
    private SerializedProperty _increasePercentProperty;
    private SerializedProperty _isBaseVersionProperty;
    private SerializedProperty _superVersionProperty; 
    private SerializedProperty _tupleItemProperty; 

    private void OnEnable()
    {
        _spriteProperty = serializedObject.FindProperty("Sprite");
        _nameProperty = serializedObject.FindProperty("Name");
        _typeProperty = serializedObject.FindProperty("_type");
        _attackProperty = serializedObject.FindProperty("_attack");
        _speedProperty = serializedObject.FindProperty("_speed");
        _lifeTimeProperty = serializedObject.FindProperty("_lifeTime");
        _radiusProperty = serializedObject.FindProperty("_radius");
        _radiusVariationProperty = serializedObject.FindProperty("_radiusVariation");
        _heightProperty = serializedObject.FindProperty("_height");
        _heightVariationProperty = serializedObject.FindProperty("_heightVariation");
        _distanceProperty = serializedObject.FindProperty("_distance");
        _spawnIntervalProperty = serializedObject.FindProperty("_spawnInterval");
        _canTurnProperty = serializedObject.FindProperty("_canTurn");
        _addProjectileWhenLevelingUpProperty = serializedObject.FindProperty("_addProjectileWhenLevelingUp");
        _increaseWhenLevelingUpProperty = serializedObject.FindProperty("_increaseWhenLevelingUp");
        _increasePercentProperty = serializedObject.FindProperty("_increasePercent");
        _isBaseVersionProperty = serializedObject.FindProperty("_isBaseVersion");
        _superVersionProperty = serializedObject.FindProperty("_superVersion");
        _tupleItemProperty = serializedObject.FindProperty("_tuplePassiveItem");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_spriteProperty, new GUIContent("Sprite"));
        EditorGUILayout.PropertyField(_nameProperty, new GUIContent("Name"));
        EditorGUILayout.PropertyField(_typeProperty, new GUIContent("Type"));
        EditorGUILayout.PropertyField(_attackProperty, new GUIContent("Attack value"));
        EditorGUILayout.PropertyField(_isBaseVersionProperty, new GUIContent("Is base version"));

        AttackType currentType = (AttackType)_typeProperty.intValue;
        bool isBaseVersion = _isBaseVersionProperty.boolValue;

        if (isBaseVersion)
        {
            EditorGUILayout.PropertyField(_superVersionProperty, new GUIContent("Super version"));
            EditorGUILayout.PropertyField(_tupleItemProperty, new GUIContent("Tuple passive item"));
        }

        if (currentType == AttackType.Static)
        {
            EditorGUILayout.PropertyField(_canTurnProperty, new GUIContent("Can turn"));
            EditorGUILayout.PropertyField(_increaseWhenLevelingUpProperty, new GUIContent("Increase when leveling up"));

            bool increaseWhenLevelingUpProperty = _increaseWhenLevelingUpProperty.boolValue;

            if (increaseWhenLevelingUpProperty)
            {
                EditorGUILayout.PropertyField(_increasePercentProperty, new GUIContent("Increase percent"));
            }
        }
        else
        {
            EditorGUILayout.PropertyField(_speedProperty, new GUIContent("Speed"));
            EditorGUILayout.PropertyField(_lifeTimeProperty, new GUIContent("Life time"));
            EditorGUILayout.PropertyField(_spawnIntervalProperty, new GUIContent("Spawn interval"));
            EditorGUILayout.PropertyField(_addProjectileWhenLevelingUpProperty, new GUIContent("Add projectile when leveling up"));
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

            float lifeTime = _lifeTimeProperty.floatValue;
            int spawnInterval = _spawnIntervalProperty.intValue;

            if(lifeTime > spawnInterval)
            {
                throw new System.ArgumentOutOfRangeException($"{nameof(lifeTime)} cant be greater {nameof(spawnInterval)}");
            }
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