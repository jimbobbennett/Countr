using System;
using System.Collections.Generic;
using MvvmCross.Core.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Test.Core;
using NUnit.Framework;

namespace Countr.Core.Tests
{
    public abstract class MvxBaseUnitTest : MvxIoCSupportingTest
    {
        protected MockDispatcher MockDispatcher { get; private set; }

        [SetUp]
        public virtual void SetUpTests()
        {
            Setup();
        }

        protected override void AdditionalSetup()
        {
            MockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
        }

        protected void AssertShowViewModel<T>()
        {
            Assert.AreEqual(1, MockDispatcher.Requests.Count);
            Assert.AreEqual(typeof(T), MockDispatcher.Requests[0].ViewModelType);
        }

        protected void AssertShowViewModelWithParameter<T>(string parameterName, string parameterValue)
        {
            AssertShowViewModel<T>();
            Assert.AreEqual(parameterValue, MockDispatcher.Requests[0].ParameterValues[parameterName]);
        }

        protected void AssertShowViewModelWithParameters<T>(Func<IDictionary<string, string>, bool> validation)
        {
            AssertShowViewModel<T>();
            Assert.AreEqual(true, validation(MockDispatcher.Requests[0].ParameterValues));
        }
    }
}