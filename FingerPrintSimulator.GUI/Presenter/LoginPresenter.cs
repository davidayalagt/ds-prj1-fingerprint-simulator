using FingerPrintSimulator.Components.Model;
using FingerPrintSimulator.Components.Services;
using FingerPrintSimulator.GUI.Model;
using FingerPrintSimulator.GUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrintSimulator.GUI.Presenter
{
    public class LoginPresenter
    {
        private ILoginView view;
        private IFingerPrintEngine model;

        public LoginPresenter(ILoginView view, IFingerPrintEngine model)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.model = model ?? throw new ArgumentNullException(nameof(model));

            view.SearchFingerPrint += OnSearchFingerPrint;
            view.AddUser += OnAddUser;

        }

        private void OnAddUser(byte[] fingerPrint, string name)
        {
            model.EnrollFingerPrint(name, fingerPrint);
        }

        private void OnSearchFingerPrint(byte[] fingerPrint)
        {
            this.view.FoundUser = model.GetInfoForFingerPrint(fingerPrint);
        }
    }
}
