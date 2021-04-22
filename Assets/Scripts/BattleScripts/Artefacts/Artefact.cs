using UnityEngine;

public abstract class Artefact : MonoBehaviour
{
    [SerializeField] protected float _helth;
    [SerializeField] protected float _effectSpeed;
    [SerializeField] protected GameObject _player;
    [SerializeField] protected GameObject _spawner;

    protected virtual void ActivatingEffect(GameObject target) { }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            _spawner.GetComponent<ArtefactsLine>().SetArtefact(gameObject);
            gameObject.SetActive(false);
        }
    }
}
