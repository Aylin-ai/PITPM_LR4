using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PITPM_LR4.ViewModels.Admins;

namespace Module_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddGoodTestMethod()
        {
            MainAdminViewModel mainAdminViewModel = new MainAdminViewModel();

            mainAdminViewModel.AddGoodCommand.Execute(mainAdminViewModel);
        }

        [TestMethod]
        public void UpdateGoodTestMethod()
        {
            MainAdminViewModel mainAdminViewModel = new MainAdminViewModel();

            mainAdminViewModel.SelectedGood = new PITPM_LR4.Models.Good() { Id = 8, Name = "Помидоры", Price = 100, Quantity = 1000 };
            mainAdminViewModel.UpdateGoodsCommand.Execute(mainAdminViewModel);
        }

        [TestMethod]
        public void DeleteGoodTestMethod()
        {
            MainAdminViewModel mainAdminViewModel = new MainAdminViewModel();

            mainAdminViewModel.SelectedGood = new PITPM_LR4.Models.Good() { Id = 8, Name = "Помидоры", Price = 100, Quantity = 1000 };
            mainAdminViewModel.DeleteGoodCommand.Execute(mainAdminViewModel);
        }
    }
}
