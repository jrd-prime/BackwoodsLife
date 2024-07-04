using BackwoodsLife.Scripts.Framework.Player;
using Game.Scripts.Player.Const;
using R3;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    // [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        private IPlayerViewModel _viewModel;
        private readonly CompositeDisposable _disposables = new();

        // Components
        private Animator _animator;
        // private NavMeshAgent _navMeshAgent;

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            // _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            _animator = gameObject.GetComponent<Animator>();

            Assert.IsNotNull(_viewModel, "ViewModel is null");

            _viewModel.PlayerPosition
                .Skip(1)
                .Subscribe(PlayerPositionHandler)
                .AddTo(_disposables);

            _viewModel.PlayAnimationByName
                .Skip(1)
                // .DistinctUntilChanged()
                .Subscribe(StartAnimation)
                .AddTo(_disposables);

            // _navMeshAgent.stoppingDistance = PlayerConst.NavMeshStopDistance;
        }

        private void PlayerPositionHandler(Vector3 position)
        {
            // _navMeshAgent.SetDestination(position);
        }

        private void FixedUpdate()
        {
            _viewModel.SetModelPosition(transform.position);

            // if (_navMeshAgent.hasPath)
            // {
            //     if (_navMeshAgent.remainingDistance <= PlayerConst.NavMeshStopDistance)
            //     {
            //         Debug.LogWarning("HAS PATH = dest reach set true");
            //         _navMeshAgent.SetDestination(transform.position);
            //         _viewModel.DestinationReached.Value = true;
            //         _viewModel.DestinationReached.Value = false;
            //     }
            // }
        }

        private void StartAnimation(string x)
        {
            Debug.LogWarning("<color=cyan>Start Animation >>> " + x + "</color>");
            _animator.CrossFade(x, PlayerConst.AnimationCrossFade);
        }

        private void OnDestroy() => _disposables.Dispose();
    }
}
