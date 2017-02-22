﻿using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Contacts.UITest
{
  [TestFixture(Platform.Android)]
  //[TestFixture(Platform.iOS)]
  public class Tests
  {
    IApp app;
    Platform platform;

    public Tests(Platform platform)
    {
      this.platform = platform;
    }

    [SetUp]
    public void BeforeEachTest()
    {
      app = AppInitializer.StartApp(platform);
    }

    [Test]
    public void AppLaunches()
    {
      //app.Repl();
      //var query = app.Query(c => c.Class("EntryEditText")); 
      app.EnterText("entUsername", "user1");
      app.EnterText("entPassword", "dasdsdsad");
      app.Tap("btnLogin");
      
    }
  }
}

