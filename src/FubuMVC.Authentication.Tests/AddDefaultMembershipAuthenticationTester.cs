﻿using FubuMVC.Authentication.Membership;
using FubuMVC.Core.Registration;
using NUnit.Framework;
using System.Linq;
using FubuTestingSupport;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class AddDefaultMembershipAuthenticationTester
    {
        [Test]
        public void insert_membership_node_when_it_is_enabled_and_missing()
        {
            var graph = new BehaviorGraph();
            graph.Settings.Get<AuthenticationSettings>()
                .MembershipEnabled = MembershipStatus.Enabled;

            new AddDefaultMembershipAuthentication().Configure(graph);

            graph.Settings.Get<AuthenticationSettings>()
                 .Strategies.Single().ShouldBeOfType<MembershipNode>();
        }

        [Test]
        public void do_not_insert_membership_node_if_there_is_already_one()
        {
            var graph = new BehaviorGraph();
            var settings = graph.Settings.Get<AuthenticationSettings>();
            settings.MembershipEnabled = MembershipStatus.Enabled;
            settings.Strategies.InsertFirst(new MembershipNode());

            new AddDefaultMembershipAuthentication().Configure(graph);

            settings.Strategies.Count().ShouldEqual(1);
        }

        [Test]
        public void do_not_insert_memebership_node_if_membership_is_disabled()
        {
            var graph = new BehaviorGraph();
            var settings = graph.Settings.Get<AuthenticationSettings>();
            settings.MembershipEnabled = MembershipStatus.Disabled;

            new AddDefaultMembershipAuthentication().Configure(graph);

            settings.Strategies.Any().ShouldBeFalse();
        }
    }
}