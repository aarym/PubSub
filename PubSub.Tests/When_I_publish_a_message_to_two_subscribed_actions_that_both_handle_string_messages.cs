﻿using System;
using Machine.Specifications;

namespace PubSub.Tests
{
    [Subject(typeof (PubSub))]
    public class When_I_publish_a_message_to_two_subscribed_actions_that_both_handle_string_messages
    {
        private static PubSub _sut;
        private static string _message;

        private static string _receivedMessageOne;
        private static Action<string> _actionOne;
        private static Action<string> _actionTwo;
        private static string _receivedMessageTwo;

        private Establish context = () =>
        {
            _sut = new PubSub();

            _message = "hello";

            _actionOne = s => _receivedMessageOne = s;
            _actionTwo = s => _receivedMessageTwo = s;

            _sut.Subscribe(_actionOne);
            _sut.Subscribe(_actionTwo);
        };

        private Because of = () => _sut.Publish(_message);

        private It should_receive_the_published_message_for_action_one =
            () => _receivedMessageOne.ShouldEqual(_message);

        private It should_receive_the_published_message_for_action_two =
            () => _receivedMessageTwo.ShouldEqual(_message);
    }
}