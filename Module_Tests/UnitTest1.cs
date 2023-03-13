using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PITPM_LR4.Models;
using System.Collections.Generic;
using PITPM_LR4.ViewModels.Admins;
using PITPM_LR4.Infrastructure.Commands;
using PITPM_LR4.Models;
using PITPM_LR4.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

            mainAdminViewModel.SelectedGood = null;
            mainAdminViewModel.DeleteGoodCommand.Execute(mainAdminViewModel);
        }

        [TestMethod]
        public void ShowGoodsTestMethod()
        {
            MainAdminViewModel mainAdminViewModel = new MainAdminViewModel();

            var Viewgoods = mainAdminViewModel.Goods;

            List<Good> list;

            using (Context db = new Context())
            {
                list = db.Goods.ToList();
            }


            Assert.AreEqual(Viewgoods.Count, list.Count);
        }
    }
}
