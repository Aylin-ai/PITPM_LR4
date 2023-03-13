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

namespace PITPM_LR4.ViewModels.Admins
{
    public class MainAdminViewModel : ViewModel
    {
        #region Данные о товарах

        public List<Good> Goods { get; set; }

        private Good selectedGood;
        public Good SelectedGood { get => selectedGood; set => Set(ref selectedGood, value); }

        private ImagePath selectedImage = null;
        public ImagePath SelectedImage { get => selectedImage; set => Set(ref selectedImage, value); }

        #endregion

        #region Список картинок

        public List<ImagePath> Images { get; set; } = new List<ImagePath>();

        #endregion

        #region Команды

        #region Команда обновления данных о товарах

        public ICommand UpdateGoodsCommand { get; }

        private bool CanUpdateGoodsCommandExecute(object p) => true;
        private void OnUpdateGoodsCommandExectuted(object p)
        {
            using (Context db = new Context())
            {
                if (SelectedImage != null)
                {
                    string[] sselectedImage = SelectedImage.Path.Split('\\');
                    SelectedGood.Image = "/Images/" + sselectedImage[sselectedImage.Length - 1];
                }
                for (int i = 0; i < db.Goods.Count(); i++)
                {
                    if (db.Goods.ToList()[i].Id == SelectedGood.Id)
                    {
                        db.Goods.AddOrUpdate(SelectedGood, db.Goods.ToList()[i]);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }


        #endregion

        #region Команда добавления товара

        public ICommand AddGoodCommand { get; }

        private bool CanAddGoodCommandExecute(object p) => true;
        private void OnAddGoodCommandExecuted(object p)
        {
            using (Context db = new Context())
            {
                Good good = new Good();
                db.Goods.Add(good);
                SelectedGood = good;

                db.SaveChanges();
            }
        }

        #endregion

        #region Команда удаления товара

        public ICommand DeleteGoodCommand { get; }

        private bool CanDeleteGoodCommandExecute(object p) => true;
        private void OnDeleteGoodCommandExectuted(object p)
        {
            using (Context db = new Context())
            {
                for (int i = 0; i < db.Goods.Count(); i++)
                {
                    if (db.Goods.ToList()[i].Id == SelectedGood.Id)
                    {
                        db.Goods.Remove(db.Goods.ToList()[i]);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }


        #endregion

        #endregion

        public MainAdminViewModel()
        {
            #region Команды

            UpdateGoodsCommand = new LambdaCommand(OnUpdateGoodsCommandExectuted, CanUpdateGoodsCommandExecute);
            AddGoodCommand = new LambdaCommand(OnAddGoodCommandExecuted, CanAddGoodCommandExecute);
            DeleteGoodCommand = new LambdaCommand(OnDeleteGoodCommandExectuted, CanDeleteGoodCommandExecute);

            #endregion

            var dir = Directory.GetFiles("D:\\Учеба\\4335\\2_семестр\\ПИТПМ\\ЛР4\\PITPM_LR4\\PITPM_LR4\\Images\\");

            foreach (var item in dir)
            {
                var fileInfo = new FileInfo(item);
                ImagePath imagePath = new ImagePath() { Path = fileInfo.FullName };
                Images.Add(imagePath);
            }

            using (Context db = new Context())
            {
                Goods = db.Goods.ToList();
            }

        }
    }
}
