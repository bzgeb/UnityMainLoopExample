using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Player _player;
    
    [Header("Enemies")]
    [SerializeField] Enemy _enemyPrefab;
    [SerializeField] int _enemiesToSpawn;
    [SerializeField] Collider _spawnArea;
    readonly List<Enemy> _enemies = new List<Enemy>();
    readonly List<Enemy> _deadEnemies = new List<Enemy>();

    [Header("Menus")]
    [SerializeField] Menu _pauseMenu;

    public Player Player => _player;

    public struct GameInput
    {
        public float Horizontal;
        public float Vertical;
        public bool Pause;
    };
    
    void Start()
    {
        _pauseMenu.OnCreated(this);
        _player.OnCreated(this);

        for (int i = 0; i < _enemiesToSpawn; ++i)
        {
            Vector3 point = _spawnArea.bounds.RandomPoint();
            point.y = 0.5f;
            Enemy enemy = Instantiate(_enemyPrefab, point, Quaternion.identity);
            enemy.OnCreated(this);
            _enemies.Add(enemy);
        }
    }

    void Update()
    {
        //Collect Input first
        GameInput gameInput = new GameInput();
        gameInput.Horizontal = Input.GetAxis("Horizontal");
        gameInput.Vertical = Input.GetAxis("Vertical");
        gameInput.Pause = Input.GetKeyDown(KeyCode.Escape);

        if (gameInput.Pause)
        {
            _pauseMenu.Toggle();
        }

        if (_pauseMenu.IsOpen)
        {
            _pauseMenu.OnUpdate(gameInput);
        }
        else
        {
            _player.OnUpdate(gameInput);
            
            foreach (Enemy enemy in _enemies)
            {
                enemy.OnUpdate();
            }

            foreach (Enemy enemy in _deadEnemies)
            {
                _enemies.Remove(enemy);
                Destroy(enemy.gameObject);
            }
            _deadEnemies.Clear();
        }
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        _deadEnemies.Add(enemy);
    }
}
