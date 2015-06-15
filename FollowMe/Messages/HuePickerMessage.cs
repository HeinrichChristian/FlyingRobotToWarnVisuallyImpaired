namespace FollowMe.Messages
{
    public class HuePickerMessage
    {
        public int HueMin { get; private set; }
        public int HueMax { get; private set; }
        public HuePickerMessageType Type { get; private set; }
        

        public HuePickerMessage(HuePickerMessageType type, int hueMin, int hueMax)
        {
            HueMin = hueMin;
            HueMax = hueMax;
            Type = type;
        }
    }

    public enum HuePickerMessageType
    {
        Person,
        Danger

    }
}
