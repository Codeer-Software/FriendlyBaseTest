using System;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendlyBaseTargetNet20;
using System.Runtime.InteropServices;

namespace Test35
{
    /// <summary>
    /// TestWindowControlのインターフェイスに対する検査
    /// </summary>
    [TestClass]
    public class TestWindowControl
    {
        /// <summary>
        /// 用意
        /// </summary>
        public WindowsAppFriend SetUp()
        {
            if (IntPtr.Size == 4)
            {
                return new WindowsAppFriend(Process.Start(TargetPath.Path32), "2.0");
            }
            else
            {
                return new WindowsAppFriend(Process.Start(TargetPath.Path64), "2.0");
            }
        }

        /// <summary>
        /// 用意
        /// </summary>
        public WindowsAppFriend SetUpWpf()
        {
            if (IntPtr.Size == 4)
            {
                return new WindowsAppFriend(Process.Start(TargetPath.PathWpf32), "4.0");
            }
            else
            {
                return new WindowsAppFriend(Process.Start(TargetPath.PathWpf64), "4.0");
            }
        }

        /// <summary>
        /// ウィンドウ情報取得メソッドテスト(ネイティブ)
        /// </summary>
        [TestMethod]
        public void TestNativeWindowInfo()
        {
            if (IntPtr.Size != 4)
            {
                return;
            }
            Process process = null;
            using (WindowsAppFriend app = new WindowsAppFriend(Process.Start(TargetPath.PathMfc), "2.0"))
            {
                process = Process.GetProcessById(app.ProcessId);
                WindowControl targetForm = app.FromZTop();
                WindowControl comboEx = targetForm.IdentifyFromDialogId(1004);
                Assert.AreEqual(1004, comboEx.DialogId);
                Assert.AreEqual("ComboBoxEx32", comboEx.WindowClassName);
                Assert.AreEqual(targetForm.Handle, comboEx.ParentWindow.Handle);
            }
            if (process != null)
            {
                process.CloseMainWindow();
            }
        }

        /// <summary>
        /// ウィンドウ情報取得メソッドテスト(ネイティブ)
        /// </summary>
        [TestMethod]
        public void TestNetWindowInfo()
        {
            Process process = null;
            using (WindowsAppFriend app = SetUp())
            {
                process = Process.GetProcessById(app.ProcessId);
                WindowControl targetForm = app.FromZTop();
                Assert.AreEqual(typeof(TargetForm).FullName, targetForm.TypeFullName);
            }
            if (process != null)
            {
                process.CloseMainWindow();
            }
        }

        /// <summary>
        /// GetTopLevelWindowsテスト
        /// </summary>
        [TestMethod]
        public void TestGetTopLevelWindow()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                AppVar newForm = app.Dim(new NewInfo<Form>());
                newForm["Text"]("abc");
                newForm["Show"]();
                WindowControl[] all = app.GetTopLevelWindows();
                Assert.AreEqual(2, all.Length);
                Assert.AreEqual(targetForm.Handle, all[0].Handle);
                Assert.AreEqual((IntPtr)newForm["Handle"]().Core, all[1].Handle);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromWindowTextテスト
        /// </summary>
        [TestMethod]
        public void TestIdentifyFromWindowText()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                AppVar newForm = app.Dim(new NewInfo<Form>());
                newForm["Text"]("abc");
                newForm["Show"]();
                WindowControl w = app.IdentifyFromWindowText("abc");
                Assert.AreEqual((IntPtr)newForm["Handle"]().Core, w.Handle);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromWindowTextの例外テスト
        /// </summary>
        [TestMethod]
        public void TestIdentifyFromWindowTextException()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                for (int i = 0; i < 2; i++)
                {
                    AppVar newForm = app.Dim(new NewInfo<Form>());
                    newForm["Text"]("abc");
                    newForm["Show"]();
                }
                try
                {
                    app.IdentifyFromWindowText("xxx");
                    Assert.Fail();
                }
                catch (WindowIdentifyException e)
                {
                    Assert.AreEqual("指定のウィンドウが見つかりませんでした。", e.Message);
                }
                try
                {
                    app.IdentifyFromWindowText("abc");
                    Assert.Fail();
                }
                catch (WindowIdentifyException e)
                {
                    Assert.AreEqual("指定のウィンドウが複数発見され、特定できませんでした。", e.Message);
                }
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// GetFromWindowTextテスト
        /// </summary>
        [TestMethod]
        public void TestGetFromWindowText()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                for (int i = 0; i < 2; i++)
                {
                    AppVar newForm = app.Dim(new NewInfo<Form>());
                    newForm["Text"]("abc");
                    newForm["Show"]();
                }
                WindowControl[] windows = app.GetFromWindowText("abc");
                Assert.AreEqual(2, windows.Length);
                Assert.AreEqual("abc", windows[0].GetWindowText());
                Assert.AreEqual("abc", windows[0].GetWindowText());
                Assert.IsFalse(windows[0].Handle == windows[1].Handle);
                Assert.AreEqual(0, app.GetFromWindowText("xxx").Length);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromTypeFullNameテスト
        /// </summary>
        [TestMethod]
        public void TestIdentifyFromTypeFullName()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                AppVar newForm = app.Dim(new NewInfo<Form>());
                newForm["Text"]("abc");
                newForm["Show"]();
                WindowControl w = app.IdentifyFromTypeFullName(typeof(Form).FullName);
                Assert.AreEqual((IntPtr)newForm["Handle"]().Core, w.Handle);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromTypeFullNameの例外テスト
        /// </summary>
        [TestMethod]
        public void TestIdentifyFromTypeFullNameException()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                for (int i = 0; i < 2; i++)
                {
                    AppVar newForm = app.Dim(new NewInfo<Form>());
                    newForm["Text"]("abc");
                    newForm["Show"]();
                }
                try
                {
                    app.IdentifyFromTypeFullName("xxx");
                    Assert.Fail();
                }
                catch (WindowIdentifyException e)
                {
                    Assert.AreEqual("指定のウィンドウが見つかりませんでした。", e.Message);
                }
                try
                {
                    app.IdentifyFromTypeFullName(typeof(Form).FullName);
                    Assert.Fail();
                }
                catch (WindowIdentifyException e)
                {
                    Assert.AreEqual("指定のウィンドウが複数発見され、特定できませんでした。", e.Message);
                }
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// GetFromTypeFullNameテスト
        /// </summary>
        [TestMethod]
        public void TestGetFromTypeFullName()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                for (int i = 0; i < 2; i++)
                {
                    AppVar newForm = app.Dim(new NewInfo<Form>());
                    newForm["Text"]("abc");
                    newForm["Show"]();
                }
                WindowControl[] windows = app.GetFromTypeFullName(typeof(Form).FullName);
                Assert.AreEqual(2, windows.Length);
                Assert.AreEqual("abc", windows[0].GetWindowText());
                Assert.AreEqual("abc", windows[0].GetWindowText());
                Assert.IsFalse(windows[0].Handle == windows[1].Handle);
                Assert.AreEqual(0, app.GetFromTypeFullName("xxx").Length);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromWindowTextテスト
        /// </summary>
        [TestMethod]
        public void TestWaitForIdentifyFromWindowText()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                AppVar newForm = app.Dim(new NewInfo<Form>());
                newForm["Text"]("abc");
                newForm["Show"]();
                WindowControl w = app.WaitForIdentifyFromWindowText("abc");
                Assert.AreEqual((IntPtr)newForm["Handle"]().Core, w.Handle);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromWindowTextテスト
        /// </summary>
        [TestMethod]
        public void TestWaitForIdentifyFromWindowTextAsync()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                AppVar newForm = app.Dim(new NewInfo<Form>());
                newForm["Text"]("abc");
                newForm["Show"]();
                WindowControl w = app.WaitForIdentifyFromWindowText("abc", new Async());
                Assert.AreEqual((IntPtr)newForm["Handle"]().Core, w.Handle);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromWindowTextテスト
        /// </summary>
        [TestMethod]
        public void TestWaitForIdentifyFromWindowTextAsyncNull()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                Async async = new Async();
                app[typeof(Thread), "Sleep", async](1000);
                WindowControl w = app.WaitForIdentifyFromWindowText("abc", async);
                Assert.IsNull(w);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromTypeFullNameテスト
        /// </summary>
        [TestMethod]
        public void TestWaitForIdentifyFromTypeFullName()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                AppVar newForm = app.Dim(new NewInfo<Form>());
                newForm["Text"]("abc");
                newForm["Show"]();
                WindowControl w = app.WaitForIdentifyFromTypeFullName(typeof(Form).FullName);
                Assert.AreEqual((IntPtr)newForm["Handle"]().Core, w.Handle);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromTypeFullNameテスト
        /// </summary>
        [TestMethod]
        public void TestWaitForIdentifyFromTypeFullNameAsync()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                AppVar newForm = app.Dim(new NewInfo<Form>());
                newForm["Text"]("abc");
                newForm["Show"]();
                WindowControl w = app.WaitForIdentifyFromTypeFullName(typeof(Form).FullName, new Async());
                Assert.AreEqual((IntPtr)newForm["Handle"]().Core, w.Handle);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// IdentifyFromTypeFullNameテスト
        /// </summary>
        [TestMethod]
        public void TestWaitForIdentifyFromTypeFullNameAsyncNull()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                Async async = new Async();
                app[typeof(Thread), "Sleep", async](1000);
                WindowControl w = app.WaitForIdentifyFromTypeFullName(typeof(Form).FullName, async);
                Assert.IsNull(w);
                targetForm["Close", new Async()]();
            }
        }

        /// <summary>
        /// 次ウィンドウ待ちテスト
        /// </summary>
        [TestMethod]
        public void TestWaitNextWindow()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                var next = app.WaitForNextWindow(() =>
                {
                    var form = app.Dim(new NewInfo<Form>());
                    form["Show"]();
                });
                next.Close();
                targetForm.Close(new Async());
            }
        }

        /// <summary>
        /// 次ウィンドウ取得テスト
        /// </summary>
        [TestMethod]
        public void TestGetNextWindow()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                var nexts = app.GetNextWindows(() =>
                {
                    app.Dim(new NewInfo<Form>())["Show"]();
                    app.Dim(new NewInfo<Form>())["Show"]();
                });
                Assert.AreEqual(2, nexts.Length);
                targetForm.Close(new Async());
            }
        }

        /// <summary>
        /// 次ウィンドウ待ちテスト
        /// </summary>
        [TestMethod]
        public void TestWaitNextWindowAsync()
        {
            using (WindowsAppFriend app = SetUp())
            {
                WindowControl targetForm = app.FromZTop();
                var async = new Async();
                var next = app.WaitForNextWindow(() =>
                {
                    var form = app.Dim(new NewInfo<Form>());
                    form["Show", async]();
                    form["Close"]();
                },
                async);
                Assert.IsNull(next);
                targetForm.Close(new Async());
            }
        }
    }
}
