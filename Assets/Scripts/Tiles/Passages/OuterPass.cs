using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterPass : Pass
{
    protected override void PlayAnimations()
    {
        print("Animation Right");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ParentTile.IsGoalAchived)
        {
            Level.Instance.ToBlackout("ToBlackoutStart");
            StartCoroutine(SetPlayerPosition(Vector2.zero));
        }
        //SwitchTile(collision.gameObject);
    }
    override protected void SwitchTile(GameObject triger)
    {
        //if (ParentTile.IsGoalAchived)
        //{
        //    if (ParentTile.NextTile == null)
        //    {
        //        Level.Instance.Instantiate();
        //    }
        //    else
        //    {
        //        ParentTile.NextTile.Enter();
        //        SetPosition();
        //    }

        //}
    }
    override public void SetPosition()
    {
        StartCoroutine(SetPlayerPosition(Level.Instance.CurrentTile.Entrance.transform.position));
    }

}
