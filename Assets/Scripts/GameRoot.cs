using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private EnemyGenerator _enemyGenerator;
    [SerializeField] private Hero _hero;
    [SerializeField] private GameObject _levelUpPanel;

    public void StartLevel(LevelUpItem levelUpItem)
    {
        _levelUpPanel.gameObject.SetActive(false);
        _enemyGenerator.Initialize();
        _hero.Initialize(levelUpItem);
    }
}
