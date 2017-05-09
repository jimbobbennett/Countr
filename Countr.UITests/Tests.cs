﻿using NUnit.Framework;
using Xamarin.UITest;

namespace Countr.UITests
{
   [TestFixture(Platform.Android)]
   [TestFixture(Platform.iOS)]
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
         app.Screenshot("First screen.");
      }

      [Test]
      public void AddingACounterAddsItToTheCountersScreen()
      {
         // Arrange
         // Act
         app.Screenshot("First screen.");
         app.Tap(c => c.Id("AddCounterButton"));
         app.Screenshot("About to enter text");
         app.EnterText(c => c.Id("NewCounterName"), "My Counter");
         app.Tap(c => c.Text("Done"));
         // Assert
         app.Screenshot("Counter added");
         app.WaitForElement(c => c.Id("CounterName").Text("My Counter"));
         app.WaitForElement(c => c.Id("CounterCount").Text("0"));
      }

      [Test]
      public void IncrementingACounterAddsOneToItsCount()
      {
         // Arrange
         app.Screenshot("First screen.");
         app.Tap(c => c.Id("AddCounterButton"));
         app.Screenshot("About to enter text");
         app.EnterText(c => c.Id("NewCounterName"), "My Counter");
         app.Tap(c => c.Text("Done"));
         app.Screenshot("Counter added");
         // Act
         app.Tap(c => c.Id("add_image"));
         app.Screenshot("Counter incremented");
         // Assert
         app.WaitForElement(c => c.Id("CounterCount").Text("1"));
      }
   }
}
