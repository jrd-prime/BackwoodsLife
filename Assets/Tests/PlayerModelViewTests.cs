using BackwoodsLife.Scripts.Data.Player;
using NUnit.Framework;
using R3;
using UnityEngine;

namespace Tests
{
    public class PlayerViewModelTests
    {
        private PlayerModel _model;
        private PlayerViewModelMock _viewModel;
        private readonly CompositeDisposable _disposables = new();

        [SetUp]
        public void SetUp()
        {
            _model = new PlayerModel();
            _viewModel = new PlayerViewModelMock(_model, 5f, 10f);
        }

        [TearDown]
        public void TearDown()
        {
            _viewModel.Dispose();
            _model.Dispose();
        }

        [Test]
        public void SetModelPosition_ShouldUpdateModelPosition()
        {
            // Arrange
            Vector3 newPosition = new Vector3(1, 2, 3);

            // Act
            _viewModel.SetModelPosition(newPosition);

            // Assert
            Assert.AreEqual(newPosition, _model.Position.Value);
            Assert.AreEqual(newPosition, _viewModel.Position.CurrentValue);
        }

        [Test]
        public void SetModelRotation_ShouldUpdateModelRotation()
        {
            // Arrange
            Quaternion newRotation = Quaternion.Euler(0, 45, 0);

            // Act
            _viewModel.SetModelRotation(newRotation);

            // Assert
            Assert.AreEqual(newRotation, _model.Rotation.Value);
            Assert.AreEqual(newRotation, _viewModel.Rotation.CurrentValue);
        }

        [Test]
        public void SetAnimation_ShouldUpdatePlayAnimationByName()
        {
            // Arrange
            string animationName = "Run";

            // Act
            _viewModel.SetAnimation(animationName);

            // Assert
            Assert.AreEqual(animationName, _viewModel.PlayAnimationByName.Value);
        }

        [Test]
        public void Dispose_ShouldDisposeReactiveProperties()
        {
            // Arrange
            var positionDisposed = false;
            var rotationDisposed = false;
            var moveDirectionDisposed = false;
            var playAnimationByNameDisposed = false;

            _viewModel.Position.Subscribe(_ => { }, _ => positionDisposed = true).AddTo(_disposables);
            _viewModel.Rotation.Subscribe(_ => { }, _ => rotationDisposed = true).AddTo(_disposables);
            _viewModel.MoveDirection.Subscribe(_ => { }, _ => moveDirectionDisposed = true).AddTo(_disposables);
            _viewModel.PlayAnimationByName.Subscribe(_ => { }, _ => playAnimationByNameDisposed = true)
                .AddTo(_disposables);

            // Act
            _viewModel.Dispose();

            // Assert
            Assert.IsTrue(positionDisposed);
            Assert.IsTrue(rotationDisposed);
            Assert.IsTrue(moveDirectionDisposed);
            Assert.IsTrue(playAnimationByNameDisposed);
        }

        [Test]
        public void MoveSpeed_ShouldBeInitialized()
        {
            // Arrange & Act
            var moveSpeed = _model.MoveSpeed.Value;

            // Assert
            Assert.AreEqual(5f, moveSpeed);
        }

        [Test]
        public void RotationSpeed_ShouldBeInitialized()
        {
            // Arrange & Act
            var rotationSpeed = _model.RotationSpeed.Value;

            // Assert
            Assert.AreEqual(10f, rotationSpeed);
        }
    }
}
