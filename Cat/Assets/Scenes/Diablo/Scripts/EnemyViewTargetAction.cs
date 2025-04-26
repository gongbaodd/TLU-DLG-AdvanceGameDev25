using Assets.Scenes.Diablo.Scripts;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Assets.Scenes.Diablo.Scripts
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "view target", story: "[Detector] finds the [target]", category: "Action", id: "34f99b21a3d5b9b70fedbb6c335fc7ea")]
    public partial class EnemyViewTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyVisionDetectorController> Detector;
        [SerializeReference] public BlackboardVariable<GameObject> Target;


        protected override Status OnUpdate()
        {
            Target.Value = Detector.Value.GetTarget();
            return Target.Value == null ? Status.Failure : Status.Success;
        }

    }

}


