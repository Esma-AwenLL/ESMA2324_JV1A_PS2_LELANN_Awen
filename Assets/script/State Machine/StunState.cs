using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMvtStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRage;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = entity.CheckGround();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRage = entity.CheckPlayerInMinAgroRange();
    }
    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMvtStopped = false;
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
    }
    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
            

        }
        if(isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMvtStopped)
        {
            isMvtStopped = true;
            entity.SetVelocity(0f);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
