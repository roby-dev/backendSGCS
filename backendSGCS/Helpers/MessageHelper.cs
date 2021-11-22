namespace backendSGCS.Helpers
{
    public class MessageHelper
    {
        public bool ok { get; set; }
        public string? msg { get; set; }

        public static MessageHelper createMessage(bool _ok, string _msg)
        {
            return new MessageHelper {
                ok = _ok,
                msg = _msg
            };
        }
    }
}