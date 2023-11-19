using Xtramile.Weather.MainApp.Core.Enum;

namespace Xtramile.Weather.MainApp.Core.Message
{
    public class Message
    {
        public MessageTypeEnum Type { get; set; }

        public string MessageText { get; set; } = string.Empty;
    }
}
