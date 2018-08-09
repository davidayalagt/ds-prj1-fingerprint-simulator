using FingerPrintSimulator.Components.Model;
using FingerPrintSimulator.Components.Services;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FingerPrintSimulator.Components.ServicesImpl
{
    public class FingerPrintEngine : IFingerPrintEngine
    {
        public IRepository<UserEntry, string> repository;

        public FingerPrintEngine(IRepository<UserEntry, string> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool EnrollFingerPrint(string name, byte[] fingerPrint)
        {
            var user = new UserEntry() { Name = name };
            user.FingerPrint = Convert.ToBase64String(fingerPrint);

            if (repository.Exists(user.FingerPrint))
            {
                return true;
            }

            try
            {
                repository.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserEntry GetInfoForFingerPrint(byte[] fingerPrint)
        {
            var fingerPrintHash = Convert.ToBase64String(fingerPrint);
            return repository.Get(fingerPrintHash);
        }
    }
}
