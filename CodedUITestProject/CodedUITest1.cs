using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITestProject
{
    /// <summary>
    /// Сводное описание для CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            // тестирование запуска программы            
            this.UIMap.OpenProgramm();
            // тестирование входа как арендодатель
            this.UIMap.LoginEmployees();
            // тестирование добавления нового клиента
            this.UIMap.EmployeesAddNewClients();
            // тестирование удаление клиента
            this.UIMap.EmployeesDeleteClients();
            // тестирование добавление нового контракта
            this.UIMap.EmployeesAddNewContracts();
            // тестирование выхода из системы
            this.UIMap.LogOut();
            // тестирование входа как инженер
            this.UIMap.LoginEngineer();
            // тестирование добавление нового авто
            this.UIMap.EngineerAddNewAuto();
            // тестирование выхода из системы
            this.UIMap.LogOut();
            // тестирование входа как администратор
            this.UIMap.LoginAdmin();
           
        }

        #region Дополнительные атрибуты тестирования


        #endregion

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
