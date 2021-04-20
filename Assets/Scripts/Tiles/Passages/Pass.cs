using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
abstract public class Pass : MonoBehaviour
{
    protected Tile ParentTile;
    protected Hero _player;
    public Animation Animation;

    abstract protected void PlayAnimations();
    public void Start()
    {
        ParentTile = GetComponentInParent<Tile>();
        _player = FindObjectOfType<Hero>();
        Animation = GetComponent<Animation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Hero_Move>() != null && ParentTile.IsGoalAchived)
        {
            SwitchTile(collision.gameObject);
        }
    }
    abstract protected void SwitchTile(GameObject triger);
    protected IEnumerator SetPlayerPosition(Vector2 position)
    {
       
        int i = 0;
        do
        {

            if (i == 1)
            {
                // _player.transform.position = position;
                SceneManager.LoadScene(Level.Instance.NextSceneName);
            }
            i++;
            yield return new WaitForSeconds(1.5f);
        } while (i <= 1);

    }
    virtual public void SetPosition()
    {

    }
}
