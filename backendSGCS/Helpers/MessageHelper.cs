using backendSGCS.Models;

namespace backendSGCS.Helpers
{
    public class MessageHelper
    {
        public static Message createMessage(bool _ok, string _msg)
        {
            return new Message
            {
                ok = _ok,
                msg = _msg
            };
        }
    }
}