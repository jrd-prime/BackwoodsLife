using NUnit.Framework;
using Moq;
using UnityEngine;
using UniRx;
using Zenject;
using System.Collections;
using BackwoodsLife.Scripts.Gameplay.Player;
using R3;
using UnityEngine.TestTools;

public class PlayerViewTests : ZenjectUnitTestFixture
{
    private Mock<IPlayerViewModel> _viewModelMock;
    private GameObject _gameObject;
    private PlayerView _playerView;

    [SetUp]
    public void SetUp()
    {
        _viewModelMock = new Mock<IPlayerViewModel>();
        _viewModelMock.Setup(vm => vm.MoveDirection).Returns(new ReactiveProperty<Vector3>());
        _viewModelMock.Setup(vm => vm.PlayAnimationByName).Returns(new ReactiveProperty<string>());
        _viewModelMock.Setup(vm => vm.MoveSpeed).Returns(5f);
        _viewModelMock.Setup(vm => vm.RotationSpeed).Returns(10f);

        _gameObject = new GameObject();
        _gameObject.AddComponent<Rigidbody>();
        _gameObject.AddComponent<Animator>();

        _playerView = _gameObject.AddComponent<PlayerView>();
        Container.Inject(_playerView);
        _playerView.Construct(_viewModelMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerView_Awake_ShouldInitializeDependencies()
    {
        yield return null;

        Assert.IsNotNull(_playerView.rb);
        Assert.IsNotNull(_playerView.animator);
        Assert.AreEqual(_viewModelMock.Object.MoveSpeed, _playerView.moveSpeed);
        Assert.AreEqual(_viewModelMock.Object.RotationSpeed, _playerView.rotationSpeed);
    }

    [UnityTest]
    public IEnumerator PlayerView_FixedUpdate_ShouldCallMove_WhenMoveDirectionIsNotZero()
    {
        // Arrange
        _viewModelMock.Setup(vm => vm.MoveDirection.Value).Returns(new Vector3(1, 0, 0));
        _playerView.Awake();
        yield return null;

        // Act
        _playerView.FixedUpdate();
        yield return null;

        // Assert
        _viewModelMock.Verify(vm => vm.SetModelPosition(It.IsAny<Vector3>()), Times.Once);
        _viewModelMock.Verify(vm => vm.SetModelRotation(It.IsAny<Quaternion>()), Times.Once);
    }

    [UnityTest]
    public IEnumerator PlayerView_FixedUpdate_ShouldNotCallMove_WhenMoveDirectionIsZero()
    {
        // Arrange
        _viewModelMock.Setup(vm => vm.MoveDirection.Value).Returns(Vector3.zero);
        _playerView.Awake();
        yield return null;

        // Act
        _playerView.FixedUpdate();
        yield return null;

        // Assert
        _viewModelMock.Verify(vm => vm.SetModelPosition(It.IsAny<Vector3>()), Times.Never);
        _viewModelMock.Verify(vm => vm.SetModelRotation(It.IsAny<Quaternion>()), Times.Never);
    }

    [UnityTest]
    public IEnumerator PlayerView_Move_ShouldUpdateRigidbodyPositionAndRotation()
    {
        // Arrange
        var moveDirection = new Vector3(1, 0, 0);
        _viewModelMock.Setup(vm => vm.MoveDirection.Value).Returns(moveDirection);
        _playerView.Awake();
        yield return null;

        // Act
        _playerView.Move();
        yield return null;

        // Assert
        Assert.AreEqual(moveDirection * _playerView.moveSpeed, _playerView.rb.position);
        _viewModelMock.Verify(vm => vm.SetModelPosition(It.IsAny<Vector3>()), Times.Once);
        _viewModelMock.Verify(vm => vm.SetModelRotation(It.IsAny<Quaternion>()), Times.Once);
    }

    [Test]
    public void PlayerView_OnDestroy_ShouldDisposeDisposables()
    {
        // Arrange
        var disposables = _playerView.GetType().GetField("_disposables", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_playerView) as CompositeDisposable;
        
        // Act
        _playerView.OnDestroy();

        // Assert
        Assert.IsTrue(disposables.IsDisposed);
    }
}

