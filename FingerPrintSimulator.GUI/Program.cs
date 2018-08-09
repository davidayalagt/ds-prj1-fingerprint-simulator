using FingerPrintSimulator.Components.Model;
using FingerPrintSimulator.Components.ServicesImpl;
using FingerPrintSimulator.GUI.View;
using FingerPrintSimulator.GUI.Presenter;
using SharpRepository.XmlRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintSimulator.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var appPath = Directory.GetCurrentDirectory();
            var repository = new XmlRepository<UserEntry, string>(appPath);
            var fingerPrintEngine = new FingerPrintEngine(repository);
            var view = new Login();
            var presenter = new LoginPresenter(view, fingerPrintEngine);
            Application.AddMessageFilter(view);

            Application.Run(view);
        }
    }
}
