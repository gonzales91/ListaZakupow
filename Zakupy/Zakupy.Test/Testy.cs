using Moq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using Zakupy.Core;
using Zakupy.Core.DataBase;
using Zakupy.Core.ViewModel;

namespace Zakupy.Test
{
    [TestFixture]
    public class Testy : MvxIoCSupportingTest
    {
        [Test]
        public void Test1()
        {
            var mock = new Mock<IZakupyDb>();        
            mock.Setup(m => m.DodajElement("element", 15.15));
        }

        [Test]
        public void Test2()
        {
            var mock = new Mock<IZakupyDb>();
            mock.Setup(m => m.CreateTables());

        }

        [Test]
        public void Test3()
        {
            var mock = new Mock<IZakupyDb>();
            mock.Setup(m => m.PobierzZakupy());
        }

        [Test]
        public void Test4()
        {
            var mock = new Mock<IZakupyDb>();
            mock.Setup(m => m.UsunElement(151515));
        }

        [Test]
        public void Test5()
        {
            var mock = new Mock<IZakupyDb>();
            var viewModel = new ListaZakupowViewModel(mock.Object);

            viewModel.DodajProdukCmd.Execute(new DodajProdukt { NazwaProduktu= "sól", Cena = 0.597 });
        }

        [Test]
        public void Test6()
        {
            var mock = new Mock<IZakupyDb>();
            var viewModel = new ListaZakupowViewModel(mock.Object);

            viewModel.UsunProduktCmd.Execute(new ZakupyViewModel { Id =1, NazwaProduktu = "róża", Cena= 0.0 });
        }
    }
}
