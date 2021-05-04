using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager _gameManager;

    [SerializeField] CharacterController _characterController;
    [SerializeField] float _moveSpeed = 2f;

    [SerializeField] Vector2 _lifeTimeRange = new Vector2(1, 5f);
    float _lifeTime;
    float _elapsed;

    [SerializeField] ParticleSystem _onDeathParticlesPrefab;
    
    public bool IsDead { get; set; }

    public void OnCreated(GameManager gameManager)
    {
        _gameManager = gameManager;
        _elapsed = 0f;
        _lifeTime = Random.Range(_lifeTimeRange.x, _lifeTimeRange.y);
    }

    public void OnUpdate()
    {
        if (IsDead) return;
        
        Transform playerTransform = _gameManager.Player.transform;
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        _characterController.Move(directionToPlayer.normalized * (_moveSpeed * Time.deltaTime));

        _elapsed += Time.deltaTime;
        if (_elapsed > _lifeTime)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(_onDeathParticlesPrefab, transform.position, Quaternion.identity);
        _gameManager.OnEnemyKilled(this);
        IsDead = true;
    }
}