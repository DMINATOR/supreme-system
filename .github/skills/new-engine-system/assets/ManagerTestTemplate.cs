namespace SupremeEngine.Test;

using SupremeEngine;

public class {SystemName}ManagerTest
{
    [Fact]
    public void {SystemName}Manager_InitialState()
    {
        // Arrange / Act
        var manager = new {SystemName}Manager();

        // Assert
        // TODO
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var manager = new {SystemName}Manager();
        // TODO: configure manager state

        // Act
        var dto = manager.ToDto();
        var restored = {SystemName}Manager.FromDto(dto);

        // Assert
        // TODO: verify restored state matches original
    }

    // TODO: Add one test per public method covering normal path + error path
}
