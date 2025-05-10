using Assets.Scenes.Diablo.Scripts;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Assets.Scenes.Diablo.Scripts
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Attack Target", story: "[Agent] attacks the [Target]", category: "Action", id: "de9095e9a48f105ce11ea62a220aadaa")]
    public partial class AttackTargetAction : Action
    {
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    protected override Status OnUpdate()
        {
            var distance = Vector3.Distance(Agent.Value.transform.position, Target.Value.transform.position);
            var lifeController = Target.Value.GetComponent<LifeBarController>();
            var config = Agent.Value.GetComponent<SkeletonController>().config;
            var gameManager = DiabloController.gameManager;
            var gameCtrl = gameManager.GetComponent<DiabloController>();

            if (distance < config.attackRange) {
                lifeController.Attacked(config.attackValue);
                gameCtrl.PlayEnemyPunchSound();
            }
            
            return Status.Success;
        }
    }
}



