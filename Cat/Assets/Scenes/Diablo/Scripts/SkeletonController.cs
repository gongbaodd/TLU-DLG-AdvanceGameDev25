using Unity.Behavior;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(SkeletonAnimatorController))]
    [RequireComponent(typeof(CursorLabelController))]
    [RequireComponent(typeof(EnemyVisionDetectorController))]
    [RequireComponent(typeof(BehaviorGraphAgent))]
    public class SkeletonController : MonoBehaviour
    {
        public EnemyConfig config;

        BehaviorGraphAgent agent;
        BlackboardVariable<Status> status;


        void Awake()
        {
            agent = GetComponent<BehaviorGraphAgent>();

        }

        void Start()
        {
            agent.BlackboardReference.GetVariable<Status>("currentStatus", out status);
            
        }

        void Update()
        {

            if (status == null) return;


        // var state = status.Value;



        //     if (state == Status.Attack)
        //     {
        //         var player = DiabloController.player;
        //         var distance = Vector2.Distance(player.transform.position, transform.position);


        //         if (distance <= config.attackRange)
        //         {

        //             var life = player.GetComponent<LifeBarController>();
        //             life.Attacked(config.attackValue);
        //         }

        //     }
        }
    }
}

