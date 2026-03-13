﻿
Light light = new Light();
// light.On();
// light.Off();

var lightOnCommand = new LightOnCommand(light);
// lightOnCommand.Execute();

var remoteControl = new RemoteControl(lightOnCommand);
remoteControl.PressButton();

public class Light
{
    public void On()
    {
        Console.WriteLine("Light is on");
    }

    public void Off()
    {
        Console.WriteLine("Light is off");
    }
}

public interface ICommand
{
    void Execute();
}

public class LightOnCommand : ICommand
{
    private readonly Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.On();
    }
}

// public class LightOffCommand : ICommand

// Dispatcher
public class RemoteControl
{
    private ICommand _command;

    public RemoteControl(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }
}

public class AlexaRemote
{
    public void VoiceCommands()
    {
        Console.WriteLine("Voice activated remote control");
    }
}