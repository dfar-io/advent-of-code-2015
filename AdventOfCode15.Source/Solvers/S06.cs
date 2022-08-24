public class S06 : BaseSolver
{
    private int _answer1;
    private int _answer2;

    public S06(string[] input) : base(input)
    {
        var lights = new bool[1000,1000];
        var lightsP2 = new int[1000,1000];

        foreach (var commandString in _input)
        {
            var command = new Command(commandString);

            if (command.CommandType == CommandType.Toggle)
            {
                ToggleLights(lights, command);
                ToggleLightsP2(lightsP2, command);
            }
            else
            {
                TurnLights(lights, command);
                TurnLightsP2(lightsP2, command);
            }
        }

        // Count the lights that are on
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 1000; j++)
            {
                if (lights[i, j])
                {
                    _answer1++;
                }
                _answer2 += lightsP2[i, j];
            }
        }
    }

    private static void ToggleLights(bool[,] lights, Command command)
    {
        for (var x = command.X1; x <= command.X2; x++)
        {
            for (var y = command.Y1; y <= command.Y2; y++)
            {
                lights[x, y] = !lights[x, y];
            }
        }
    }

    private static void ToggleLightsP2(int[,] lights, Command command)
    {
        for (var x = command.X1; x <= command.X2; x++)
        {
            for (var y = command.Y1; y <= command.Y2; y++)
            {
                lights[x, y] = lights[x, y] + 2;
            }
        }
    }

    private static void TurnLights(bool[,] lights, Command command)
    {
        for (var x = command.X1; x <= command.X2; x++)
        {
            for (var y = command.Y1; y <= command.Y2; y++)
            {
                lights[x, y] = command.IsTurnOn;
            }
        }
    }

    private static void TurnLightsP2(int[,] lights, Command command)
    {
        for (var x = command.X1; x <= command.X2; x++)
        {
            for (var y = command.Y1; y <= command.Y2; y++)
            {
                lights[x, y] = command.IsTurnOn ? lights[x, y] + 1 : Math.Max(lights[x, y] - 1, 0);
            }
        }
    }

    public override int Answer1 => _answer1;

    public override int Answer2 => _answer2;
}

class Command
{
    public CommandType CommandType { get; set; }
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }

    public Command(string input)
    {
        var splits = input.Split(" ");
        var start = new string[0];
        var end = new string[0];

        if (splits.Count() == 4)
        {
            CommandType = CommandType.Toggle;
            start = splits[1].Split(",");
            end = splits[3].Split(",");
        }
        else
        {
            CommandType = splits[1] == "on" ? CommandType.On : CommandType.Off;
            start = splits[2].Split(",");
            end = splits[4].Split(",");
        }

        X1 = int.Parse(start[0]);
        Y1 = int.Parse(start[1]);
        X2 = int.Parse(end[0]);
        Y2 = int.Parse(end[1]);
    }

    public bool IsTurnOn => CommandType == CommandType.On;
}

enum CommandType
{
    On,
    Off,
    Toggle
}