[System.Serializable]
public class Value_Int
{
    public int min;
    public int max;
    public int value { get; private set; }

    public Value_Int(int min, int max, int value)
    {
        this.min = min;
        this.max = max;
        this.value = value;
    }

    public int SetValue(int newValue)
    {
        value = newValue;

        if (value > max) value = max;
        if (value < min) value = min;

        return value;
    }

    public int ModifyValue(int modifier)
    {
        if (value > max) value = max;
        else if (value < min) value = min;

        return SetValue(value + modifier);
    }
}