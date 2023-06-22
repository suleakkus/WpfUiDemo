using Common.Models;
using Prism.Events;

namespace Common;

public static class Events
{
    public class LoginEvent : PubSubEvent<LoginModel> { }
    public class NavigateToSignUpEvent : PubSubEvent { }
    public class NavigateToLoginEvent : PubSubEvent<LoginModel> {}
}