using BackwoodsLife.Scripts.Data.Player;
using NUnit.Framework;
using R3;
using UnityEngine;

namespace Tests
{
    public class PlayerModelTests
    {
        private PlayerModel _playerModel;
        private readonly CompositeDisposable _disposables = new();

        [SetUp]
        public void SetUp()
        {
            _playerModel = new PlayerModel();
        }

        [TearDown]
        public void TearDown()
        {
            _playerModel.Dispose();
        }

        [Test]
        public void SetPosition_ShouldUpdatePosition()
        {
            // Arrange
            Vector3 newPosition = new Vector3(1, 2, 3);

            // Act
            _playerModel.SetPosition(newPosition);

            //Assert
            Assert.AreEqual(newPosition, _playerModel.Position.Value);
        }

        [Test]
        public void SetPosition_ShouldNotifySubscribers()
        {
            // Arrange
            Vector3 newPosition = new Vector3(1, 2, 3);
            bool wasNotified = false;
            _playerModel.Position.Subscribe(pos => wasNotified = true);

            // Act
            _playerModel.SetPosition(newPosition);

            // Assert
            Assert.IsTrue(wasNotified);
        }

        [Test]
        public void SetRotation_ShouldUpdateRotation()
        {
            // Arrange
            Quaternion newRotation = Quaternion.Euler(0, 45, 0);

            // Act
            _playerModel.SetRotation(newRotation);

            // Assert
            Assert.AreEqual(newRotation, _playerModel.Rotation.Value);
        }

        [Test]
        public void SetRotation_ShouldNotifySubscribers()
        {
            // Arrange
            Quaternion newRotation = Quaternion.Euler(0, 45, 0);
            bool wasNotified = false;

            _playerModel.Rotation.Subscribe(_ => wasNotified = true);

            // Act
            _playerModel.SetRotation(newRotation);

            // Assert
            Assert.IsTrue(wasNotified);
        }

        [Test]
        public void SetMoveDirection_ShouldUpdateMoveDirection()
        {
            // Arrange
            Vector3 newMoveDirection = new Vector3(1, 0, 0);

            // Act
            _playerModel.SetMoveDirection(newMoveDirection);

            // Assert
            Assert.AreEqual(newMoveDirection, _playerModel.MoveDirection.Value);
        }

        [Test]
        public void SetMoveDirection_ShouldNotifySubscribers()
        {
            // Arrange
            Vector3 newMoveDirection = new Vector3(1, 0, 0);
            bool wasNotified = false;
            _playerModel.MoveDirection.Subscribe(_ => wasNotified = true);

            // Act
            _playerModel.SetMoveDirection(newMoveDirection);

            // Assert
            Assert.IsTrue(wasNotified);
        }

        [Test]
        public void Dispose_ShouldDisposeReactiveProperties()
        {
            // Arrange
            var positionDisposed = false;
            var rotationDisposed = false;
            var moveDirectionDisposed = false;

            _playerModel.Position.Subscribe(_ => { }, _ => positionDisposed = true).AddTo(_disposables);
            _playerModel.Rotation.Subscribe(_ => { }, _ => rotationDisposed = true).AddTo(_disposables);
            _playerModel.MoveDirection.Subscribe(_ => { }, _ => moveDirectionDisposed = true).AddTo(_disposables);

            // Act
            _playerModel.Dispose();

            // Assert
            Assert.IsTrue(positionDisposed);
            Assert.IsTrue(rotationDisposed);
            Assert.IsTrue(moveDirectionDisposed);
        }
    }
}
