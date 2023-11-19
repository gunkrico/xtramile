using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xtramile.Weather.MainApp.Core.Enum;
using Xtramile.Weather.MainApp.Core.Message;

namespace Xtramile.Weather.MainApp.ServiceContract.Response
{
    public class BaseResponse
    {
        #region Fields

        private Collection<Message> messages = new Collection<Message>();

        #endregion

        #region Properties

        public Collection<Message> Messages => messages ?? (messages = new Collection<Message>());

        #endregion

        #region Public Methods

        public bool IsError()
        {
            return Messages.Count(item => item.Type == MessageTypeEnum.Error) > 0;
        }

        public bool IsContainInfo()
        {
            return Messages.Count(item => item.Type == MessageTypeEnum.Info) > 0;
        }

        public string[] GetMessageTextArray()
        {
            return Messages.Select(item => item.MessageText).ToArray();
        }

        public string[] GetMessageErrorTextArray()
        {
            return Messages.Where(item => item.Type == MessageTypeEnum.Error)
                .Select(item => item.MessageText)
                .ToArray();
        }

        public string[] GetMessageInfoTextArray()
        {
            return Messages.Where(item => item.Type == MessageTypeEnum.Info)
                .Select(item => item.MessageText)
                .ToArray();
        }

        public string GetErrorMessage()
        {
            var messageBuilder = new StringBuilder();
            foreach (var message in Messages)
            {
                messageBuilder.AppendLine(message.MessageText);
            }

            return messageBuilder.ToString().Trim();
        }

        public void AddErrorMessage(string errorMessage)
        {
            Messages.Add(new Message
            {
                MessageText = errorMessage,
                Type = MessageTypeEnum.Error,
            });
        }

        public void AddInfoMessage(string infoMessage)
        {
            Messages.Add(new Message
            {
                MessageText = infoMessage,
                Type = MessageTypeEnum.Info,
            });
        }

        #endregion
    }
}
