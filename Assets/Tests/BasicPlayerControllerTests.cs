using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BasicPlayerControllerTests
{
    private PlayerController playerController;
    private GameObject player;

    [SetUp]
    public void Setup()
    {
        player = new GameObject();

        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<SpriteRenderer>();
        player.AddComponent<Animator>();

        playerController = player.AddComponent<PlayerController>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void PlayerController_InitJumpForceIsCorrect()
    {
        float expectedJumpForce = 5f;
        float actualJumpForce = playerController.JumpForce;

        // Use the Assert class to test conditions
        Assert.AreEqual(expectedJumpForce, actualJumpForce);
    }

    [Test]
    public void PlayerController_ActivatesJumpForceChange_ChangesJumpForce()
    {
        float expectedJumpForce = playerController.JumpForcePowerup;

        playerController.JumpForceChange();
        float changedJumpForce = playerController.JumpForce;

        Assert.AreEqual(expectedJumpForce, changedJumpForce);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerController_JumpForcePowerupTimer_WorksCorrectly()
    {
        float initTimer = 0.5f;
        float jumpForce = playerController.JumpForce;

        playerController.initalPowerupDuration = initTimer;

        playerController.JumpForceChange();

        float changedJumpForce = playerController.JumpForce;
        float currentTimer = playerController.CurrentPowerupDuration;

        Assert.AreEqual(initTimer - Time.deltaTime, currentTimer);
        Assert.AreNotEqual(jumpForce, changedJumpForce);

        playerController.JumpForceChange();

        float changedJumpForce2 = playerController.JumpForce;
        currentTimer = playerController.CurrentPowerupDuration;

        Assert.AreEqual((initTimer + initTimer) - Time.deltaTime - Time.deltaTime, currentTimer, 0.001f);
        Assert.AreEqual(changedJumpForce, changedJumpForce2);

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSecondsRealtime(currentTimer);

        Assert.AreEqual(0, playerController.CurrentPowerupDuration);
        Assert.AreEqual(playerController.JumpForce, jumpForce);
    }
}
